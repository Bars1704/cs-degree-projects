using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyNeyralNetwork
{
    class DataExample
    {
        public double[] Input { get; set; }
        public double[] Output { get; set; }
        public DataExample(double[] i, double[] o)
        {
            Input = i;
            Output = o;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var x in Input)
            {
                result.Append(x);
                result.Append(' ');
            }
            result.Append('/');
            foreach (var x in Output)
            {
                result.Append(' ');
                result.Append(x);
            }
            return result.ToString();
        }
    }






    class test
    {
        List<DataExample> CreateTicTacToeExample()
        {
            return new List<DataExample>
            {
                new DataExample(new double[]{
                                0,0,0,
                                0,0,0,
                                0,0,0,

                                0,0,0,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                1,0,0,
                                0,1,0,
                                0,0,0
                               }
                    ),
                 new DataExample(new double[]{
                                0,0,0,
                                0,0,0,
                                0,0,0,

                                1,0,0,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,1,1,
                                0,1,0,
                                0,0,0
                               }
                    ),
                  new DataExample(new double[]{
                                0,0,0,
                                0,0,0,
                                0,0,0,

                                0,0,0,
                                0,1,0,
                                0,0,0
                               },
                               new double[]
                               {
                                1,1,0,
                                0,0,0,
                                0,0,0
                               }
                    ),

                    new DataExample(new double[]{
                                1,0,0,
                                0,0,0,
                                0,0,0,

                                0,0,0,
                                0,1,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,0,0,
                                0,0,0,
                                0,0,1
                               }
                    ),

                     new DataExample(new double[]{
                                1,0,0,
                                0,0,0,
                                0,0,0,

                                0,0,1,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,0,0,
                                0,1,0,
                                1,0,0
                               }
                    ),

                      new DataExample(new double[]{
                                1,0,0,
                                0,0,0,
                                0,0,0,

                                0,1,0,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,0,0,
                                0,0,0,
                                1,0,0
                               }
                    ),

                       new DataExample(new double[]{
                                0,0,0,
                                0,1,0,
                                0,0,0,

                                1,0,0,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,0,1,
                                0,0,0,
                                0,0,0
                               }
                    ),

                        new DataExample(new double[]{
                                0,0,0,
                                0,1,0,
                                0,0,0,

                                0,1,0,
                                0,0,0,
                                0,0,0
                               },
                               new double[]
                               {
                                0,0,0,
                                0,0,0,
                                1,0,0
                               }
                    ),

                         new DataExample(new double[]{
                                0,0,0,
                                0,1,0,
                                0,0,0,

                                1,0,0,
                                0,0,0,
                                0,0,1
                               },
                               new double[]
                               {
                                0,0,0,
                                1,0,0,
                                1,0,0
                               }
                    ),

                          new DataExample(new double[]{
                                0,0,1,
                                0,0,0,
                                0,0,0,

                                1,0,0,
                                0,0,0,
                                1,0,0
                               },
                               new double[]
                               {
                                0,1,0,
                                1,0,0,
                                0,0,0
                               }
                    ),
            };
        }

    }
}