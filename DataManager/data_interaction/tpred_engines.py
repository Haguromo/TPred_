from sqlalchemy import create_engine
from DataManager.config import *


def mssql_engine():
    connctionString = 'mssql+pyodbc://{0}:{1}@{2}/{3}?{4}'.format(user, password, server, db_name, driver)
    return create_engine(connctionString, echo=True)
