from sqlalchemy import Column, Integer, String, ForeignKey, Boolean
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

from DataManager.data_interaction.tpred_engines import mssql_engine

Base = declarative_base()

class ArticleTagRelation(Base):
    __tablename__ = 'ArticleTagRelations'
    article_id = Column(Integer, ForeignKey('Articles.Id'), name="ArticleId", primary_key=True)
    tag_id = Column(Integer, ForeignKey('Tags.Id'), name="TagId", primary_key=True)

class Article(Base):
    __tablename__ = 'Articles'
    id = Column(Integer, primary_key=True, name="Id")
    name = Column(String, nullable=True, name="Name")
    text = Column(String, nullable=True, name="Text")
    source_name = Column(String, nullable=True, name="SourceName")
    url = Column(String, nullable=True, name="Url")
    processed = Column(Boolean, nullable=True, name="Processed")
    tags = relationship("Tag",
                         secondary="ArticleTagRelations", backref="Articles")

class Tag(Base):
    __tablename__ = 'Tags'
    tag = Column(String, nullable=True, name="TagName")
    id = Column(Integer, primary_key=True, name="Id", unique=True)
    articles = relationship("Article",
                             secondary="ArticleTagRelations", backref="Tags")

def recreate_db():
    engine = mssql_engine()
    Base.metadata.drop_all(engine)
    Base.metadata.create_all(engine)

