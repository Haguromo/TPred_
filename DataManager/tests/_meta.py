from sqlalchemy.orm import Session

from DataManager.data_interaction.dbfirst_models import Article, Tag
from DataManager.data_interaction.interactors import MSSQLInteractor

Interactor = MSSQLInteractor()

def _clear_db():
    session = Session(bind=Interactor.engine)
    arts = session.query(Article).filter(Article.SourceName == 'local')
    for art in arts:
        session.delete(art)

    tags = session.query(Tag)
    for tag in tags:
        if tag.TagName.startswith('test'):
            session.delete(tag)
    session.commit()

def test_call(func):
    def wrapped(f1, f2):
        res = f1()
        f2()
        return res
    def wrapper():
        return wrapped(func, _clear_db)
    return wrapper