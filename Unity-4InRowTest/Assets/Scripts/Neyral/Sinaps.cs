using System;
using System.Collections.Generic;
using System.Text;

namespace MyNeyralNetwork
{
    class Sinaps
    {
        static readonly Random rand = new Random();
        NeyralNetworkConfig Config { get; set; }
        public Neyron Output { get; set; }
        public double Weight { get; set; }
        public double LastDelt { get; set; }
        double Gradient(double inputValue)
        {
            return Output.Delta * inputValue;
        }
        public static implicit operator double(Sinaps s)
        {
            return s.Weight;
        }
        public void UpdateWeight(double inputValue)
        {
            double delt = (Config.LearningRate * Gradient(inputValue)) + (Config.Impuls * LastDelt);
            LastDelt = delt;
            Weight += delt;
        }

   
        double RandomWeight()
        {
            return ((rand.NextDouble() * 2) - 1);
        }
        public override string ToString()
        {
            return Weight.ToString();
        }
        public Sinaps(NeyralNetworkConfig config, Neyron outputNeuron)
        {
            Output = outputNeuron;
            Weight = RandomWeight();
            Config = config;
        }
    }
}
