from DataManager.config import *
from DataManager.core.classifier import TextClassifier
from DataManager.data_interaction.sample_provider import get_sample_csv

x_train, y_train = get_sample_csv(training_path)

def new_classifier():
    global classifier
    classifier = TextClassifier(epochs=epochs)
    classifier.train(x_train, y_train)
    classifier.save_model(model_path)
    return classifier


def existing_classifier():
    global classifier
    classifier = TextClassifier(threshold=activation_threshold)
    classifier.context(x_train, y_train)
    classifier.load_model(model_path)
    return classifier