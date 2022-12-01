using System;
using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Program
    {
        static void Main(string[] args)
        {
            // var queryAB = new Child(new Child(new Child(new Child(new Root("./example.xml"), "a"), "b"), "b"), "e");
            // var queryABB = new Child(new Child(new Child(new Root("./example.xml"), "a"), "b"), "b");
            // var queryAB_C = new Filter(new Child(new Child(new Root("./example.xml"), "a"), "b"), new Child(new Context(), "c"));
            // var queryADescC = new Child(new Descendant(new Child(new Root("./example.xml"), "a"), "c"), "e");

            var queryAandC = new Filter(new Child(new Child(new Child(new Root("./example.xml"), "a"), "b"), "b"), new And(new Child(new Context(), "c"), new Child(new Context(), "d")));

            /* //b[./c and ./d]  */
            var queryAnd2 = new Filter(new Descendant(new Root("./example2.xml"), "b"), new And(new Child(new Context(), "c"), new Child(new Context(), "d")));


            // printResult(queryAB);
            // printResult(queryABB);
            // printResult(queryAB_C);
            // printResult(queryADescC);
            // printResult(queryAandC);
            // printResult(queryAnd2);

            // var tstQuery = new Filter(new Descendant(new Root("./example.xml"), "b"), new And(new Child(new Context(), "c"), new Child(new Context(), "d")));
            // printResult(tstQuery);


            /* DOC//person[./address[./city and ./country] and ./profile[./interest and ./business and ./gender]]/name */
            /*    //person[./address[./city and ./country] and ./profile[./interest and ./business and ./gender]]/name  */
            var xmarkRoot = new Root("./xmark.xml");
            var xmark1_filter_address = new And(
                new Filter(
                    new Child(new Context(), "address"),
                    new And(
                        new Child(new Context(), "city"), new Child(new Context(), "country")
                    )
                ),
                new Filter(
                    new Child(new Context(), "profile"),
                    new And(
                        new And(
                            new Child(new Context(), "interest"),
                            new Child(new Context(), "business")
                        ),
                        new Child(new Context(), "gender")
                    )
                )
            );
            var xmark1 = new Child(new Filter(new Descendant(xmarkRoot, "person"), xmark1_filter_address), "name");
            // Console.WriteLine(xmark1.ToString());
            printResult(xmark1);

            /* DOC//item[./payment and .//listitem/text/bold and .//mail[./from and ./to and ./date]]/name */
            /*    //item[./payment and .//listitem/text/bold and .//mail[./from and ./to and ./date]]/name */
            var xmark2 = new Child(
                new Filter(
                    new Descendant(xmarkRoot, "item"),
                    new And(
                        new And(
                            new Child(new Context(), "payment"),
                            new Child(new Child(new Descendant(new Context(), "listitem"), "text"), "bold")
                        ),
                        new Filter(
                            new Descendant(new Context(), "mail"),
                            new And(
                                new And(
                                    new Child(new Context(), "from"),
                                    new Child(new Context(), "to")
                                ),
                                new Child(new Context(), "date")
                            )
                        )
                    )
                ),
                "name"
            );
            // Console.WriteLine(xmark2.ToString());
            printResult(xmark2);
        }

        static void printResult(BasicOperator result)
        {
            var counter = 0;
            Console.WriteLine("Result of " + result.ToString() + " query:");
            foreach (XmlNode node in result)
            {
                counter += 1;
                Console.WriteLine(node.OuterXml);
            }
            Console.WriteLine("Total results: " + counter);
            Console.WriteLine();
        }
    }
}
