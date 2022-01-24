class TArticle:
    def __init__(self, name='', url="", text="", source_name="", tags=[]):
        self.name = name
        self.url = url
        self.text = text
        self.tags = tags
        self.source_name = source_name

    def add_tag(self, tag):
        self.tags.append(tag)

