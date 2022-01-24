import pandas as pd


def get_sample_csv(path):
    """
    :param path: path to csv file
    file format: first column - labels
                second column - text
    :return:
            returns two values:
            x_train - input text
            y_train - output word
    """
    colnames = ['v1', 'v2']
    df = pd.read_csv(path, names=colnames, delimiter=',', encoding='latin-1')
    x_train = df.v2
    labels = df.v1
    y_train = [str(label).split('$') for label in labels]
    return x_train, y_train



