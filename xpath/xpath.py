import lxml.etree
xmark = lxml.etree.parse('xmark.xml')

# xpath = '//person[./address[./city and ./country] and ./profile[./interest and ./business and ./gender]]/name'
xpath = '//item[./payment and .//listitem/text/bold and .//mail[./from and ./to and ./date]]/name'

for e in xmark.xpath(xpath):
    print(f'<{e.tag}>{e.text}</{e.tag}>')
