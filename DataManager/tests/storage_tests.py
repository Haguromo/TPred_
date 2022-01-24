import unittest

from sqlalchemy import select

from DataManager.data_interaction.DTO import TArticle
from DataManager.data_interaction.dbfirst_models import Article, Tag
from DataManager.tests._meta import Interactor, test_call


class TestInteractorMethods(unittest.TestCase):

    @staticmethod
    def _articles():
        articles = [TArticle(name='TestArticle1', text='test1', url=r'http/test1', source_name='local'),
                    TArticle(name='TestArticle2', text='test2', url=r'http/test2', source_name='local'),
                    TArticle(name='TestArticle3', text='test3', url=r'http/test3', source_name='local')]
        articles[0].tags = ['test1', 'test2']
        articles[1].tags = ['test2', 'test3']
        articles[2].tags = ['test3']
        return articles

    def test_add_articles(self):
        @test_call
        def call():
            Interactor.add_processed_articles(self._articles())
            conn = Interactor.engine.connect()
            result = conn.execute(select([Article]).where(Article.Processed == True))
            check = [res for res in result]
            conn.close()
            return check

        check = call()
        self.assertEqual(len(check), 3)

    def test_add_tags(self):
        @test_call
        def call():
            Interactor.add_processed_articles(self._articles())
            conn = Interactor.engine.connect()
            result = conn.execute(select([Tag]))
            check = [res for res in result if res.TagName.startswith('test')]
            conn.close()
            return check

        check = call()
        self.assertEqual(len(check), 3)



