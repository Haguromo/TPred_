from sqlalchemy import select
from sqlalchemy.orm import Session
from DataManager.data_interaction.DTO import TArticle
from DataManager.data_interaction.dbfirst_models import Tag, Article
from DataManager.data_interaction.tpred_engines import mssql_engine
import asyncio


class _Interactor(object):
    def __init__(self):
        self.engine = None

    def add_processed_articles(self, articles):
        """
        :param articles: list of articles with defined metadata
        """
        if len(articles) is 0:
            return
        ioloop = asyncio.get_event_loop()

        tasks = [ioloop.create_task(self._add_article_async(art)) for art in articles]
        wait_tasks = asyncio.wait(tasks)
        ioloop.run_until_complete(wait_tasks)

    def get_new_articles(self):
        """
        :return: list of unprocessed articles
        """
        conn = self.engine.connect()

        result = conn.execute(select([Article]).where(Article.Processed == False))

        uprocessed = []
        for row in result:
            uprocessed.append(Article(row[0], row[1], row[2], row[3]))

        return uprocessed

    async def _add_article_async(self, article: TArticle):
        session = Session(bind=self.engine)

        art = Article(Name=article.name, Url=article.url,
                      SourceName=article.source_name, Text=article.text,
                      Processed=True)

        existingArts = session.query(Tag)
        added = set()
        artTags = []
        for tag in existingArts:
            if tag.TagName in article.tags:
                artTags.append(tag)
                art.tags_collection.append(tag)
                added.add(tag.TagName)

        for tag in article.tags:
            if tag not in added:
                art.tags_collection.append(Tag(TagName=tag))
        session.add(art)
        session.commit()
        await asyncio.sleep(0)


class MSSQLInteractor(_Interactor):
    def __init__(self):
        self.engine = mssql_engine()
