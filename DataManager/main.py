import DataManager.services as serv


class TPredServices:
    def __init__(self):
        self._services = [service for service in serv.service_generator()]

    def execute(self):
        for service in self._services:
            service()


if __name__ == "__main__":
    TPredServices().execute()
