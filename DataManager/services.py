import types


def service_generator():
    for name, val in globals().items():
        if isinstance(val, types.FunctionType) and val.__name__ is not 'serviceGenerator':
            if val.__name__.endswith('_serv'):
                yield val

def tagging_serv():
    from DataManager.core import existing_classifier
    classifier = existing_classifier()

    from DataManager.data_interaction.interactors import MSSQLInteractor
    interactor = MSSQLInteractor()

    articles = interactor.get_new_articles()

    for i in enumerate(articles):
        articles[i].tags = classifier.classify(articles[i].text)

    interactor.add_processed_articles(articles)
