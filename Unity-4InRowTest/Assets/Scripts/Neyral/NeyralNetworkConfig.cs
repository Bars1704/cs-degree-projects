using System;
using System.Collections.Generic;
using System.Text;

namespace MyNeyralNetwork
{
     class NeyralNetworkConfig
    {
        public double LearningRate;
        public double Impuls;
        public NeyralNetworkConfig(double lr, double imp)
        {
            Impuls = imp;
            LearningRate = lr;
        }
    }
}
