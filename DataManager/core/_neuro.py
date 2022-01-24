from keras import Input, Model, Sequential
from keras.engine.saving import load_model
from keras.layers import Embedding, LSTM, Dense, Activation, Dropout
from keras.optimizers import RMSprop

from DataManager.config import *

def NN(max_words, max_classes):
    model = Sequential()
    model.add(Dense(hidden_layer, input_shape=(max_words,)))
    model.add(Activation('relu'))
    model.add(Dropout(Dropout_coef))
    model.add(Dense(hidden_layer))
    model.add(Activation('relu'))
    model.add(Dropout(Dropout_coef))
    model.add(Dense(max_classes))
    model.add(Activation('softmax'))
    return model

class Classifier(object):

    def __init__(self, epochs, max_words=15000):
        self._max_words = max_words
        self._epochs = epochs

    def _warm_up_(self, x_train, y_train, max_classes):
        """
        Creates neural network
        and trains it on x/y_train datasets
        x_train - input
        y_train - expected output
        """
        self._model = NN(self._max_words, max_classes)
        self._model.compile(loss='binary_crossentropy', optimizer=RMSprop(lr=0.001), metrics=['accuracy'])

        self._model.fit(x_train, y_train,
                            batch_size=30,
                            epochs=self._epochs,
                            verbose=1,
                            validation_split=0.1)


    def save_model(self, path):
        self._model.save(path)

    def load_model(self, path):
        self._model = load_model(path)