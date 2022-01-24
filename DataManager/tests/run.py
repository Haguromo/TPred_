import unittest

from DataManager.tests.storage_tests import TestInteractorMethods


def suite():
    suite = unittest.TestSuite()
    suite.addTest(TestInteractorMethods('test_add_articles'))
    suite.addTest(TestInteractorMethods('test_add_tags'))
    return suite

if __name__ == "__main__":
    runner = unittest.TextTestRunner(failfast=True)
    runner.run(suite())
