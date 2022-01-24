#NN configurations
hidden_layer = 1000
Dropout_coef = 0.01

#Training configurations
training_path = r"..\DataManager\sample\TrainingSample.csv"
test_path = r"..\DataManager\sample\TrainingSample.csv"
epochs = 60

#Classifier configurations
activation_threshold = 0.01

model_path = r"..\DataManager\intermediate\model_neuro.h5"

#Database configurations
db_name = "TPredDB"
user = "pavlo"
password = "Mmmm0212"
server= 'servertpred.database.windows.net,1433'
driver= 'Driver={SQL Server}'