from DataManager.config import *
from DataManager.core import new_classifier, existing_classifier
from DataManager.data_interaction.sample_provider import get_sample_csv

if __name__ == "__main__":
    global x_train, y_train
    x_train, y_train = get_sample_csv(training_path)

    classifier = existing_classifier()

    x_test, y_test = get_sample_csv(test_path)
    for i in range(len(x_test)):
        prediction = classifier.classify(x_test[i])
        print(prediction, "   |  ", y_test[i])


