from sqlalchemy.ext.automap import automap_base
from DataManager.data_interaction.tpred_engines import mssql_engine

global Base
Base = automap_base()
Base.prepare(mssql_engine(), reflect=True)

Article = Base.classes.Articles
Tag = Base.classes.Tags






