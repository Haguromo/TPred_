from keras.preprocessing import sequence
from keras.preprocessing.text import Tokenizer
from sklearn.preprocessing import LabelBinarizer, MultiLabelBinarizer
from DataManager.core._neuro import Classifier

class TextClassifier(Classifier):
    def __init__(self, epochs=30, threshold=0.55, max_words=15000):
        Classifier.__init__(self, epochs, max_words)
        self.threshold = threshold
        self.le = MultiLabelBinarizer()
        self.tok = Tokenizer(num_words=self._max_words)
    @staticmethod
    def _prepare_text(text):
        text = text.lower()
        exp = lambda x: x != ',' and x != '?' and \
                        x != '!' and x != ';' and \
                        x != '"' and x != '(' and \
                        x != ')' and x != ':'
        filtered = filter(exp, text)
        res = ""
        for str in filtered:
            res += str
        return res

    @staticmethod
    def _uniq(input):
        output = []
        for x in input:
            if x not in output:
                output.append(x)
        return output

    def context(self, x_train, y_train):
        self.le.fit(y_train)
        self.tags = self.le.classes_
        self.tok.fit_on_texts([str(x) for x in x_train])

    def train(self, x_train, y_train):
        """
        Creates neural network
        and trains it on x/y_train datasets
        x_train - input
        y_train - expected output
        x_train and y_train is a text
        threshold - [0..1] threshold for getting output from classifier
        default value of threshold - 0.5
        """

        x_train = [self._prepare_text(str(x)) for x in x_train]
        self.context(x_train, y_train)
        self.tags = self.le.classes_
        max_classes = len(self.tags)

        y = self.le.transform(y_train)
        x = self.tok.texts_to_matrix(x_train)
        self._warm_up_(x, y, max_classes)

    def classify(self, text):
        """
        :param text: text to be classified on tags
        :return: tags of this text
        """
        input = [self._prepare_text(str(text))]
        x = self.tok.texts_to_matrix(input)
        output=self._model.predict(x)
        answer=self._transform_output_(output[0], self.threshold)
        return answer

    def _transform_output_(self, output, threshold=0.5):
        answer=[]
        ind=0
        for out in output:
            if out>threshold:
                answer.append(self.tags[ind])
            ind+=1
        return answer


