using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeyralNetwork
{
    class NeyralNetwork
    {
        public NeyralNetworkConfig Config { get; set; }
        public double Impuls { get; set; }
        public double LearningRate { get; set; }
        public Layer[] Layers { get; set; }
        public int Capacity { get { return Layers.Length; } }
        public int InputNeyronsCount { get; set; }
        double[] Sigmoid(double[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = Sigmoid(x[i]);
            }
            return x;
        }
        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(x * -1));
        }
        public override string ToString()
        {
            string result = "Neyrons on layers: ";
            for (int i = 0; i < Capacity; i++)
            {
                result += string.Format("{0} ", Layers[i].Capacity);
            }
            return result;
        }
        public Layer this[int index]
        {
            get
            {
                return Layers[index];
            }
            set
            {
                Layers[index] = value;
            }
        }
        public double MSE(List<DataExample> dataset)
        {
            double error = 0;
            foreach (var data in dataset)
            {
                var realResult = GetResult(data.Input);
                double currentError = data.Output.Sum(x => x) - realResult.Sum(x => x);
                error += (currentError * currentError);
            }
            error /= dataset.Count;
            return error;
        }
        public double[] GetResult(double[] inputSignals)
        {
            //inputSignals = Sigmoid(inputSignals);
            Layers[0].UpdateValues(inputSignals);
            for (int i = 1; i < Capacity; i++)
            {
                Layers[i].UpdateValues(Layers[i - 1].Signals);
            }
            return Layers[Capacity - 1].Signals;
        }
        public void Learn(DataExample data)
        {
            GetResult(data.Input);
            Layers[Layers.Length -1 ].UpdateOutputDelta(data.Output);
            for (int i = 2; i <= Capacity; i++)
            {
                Layers[Layers.Length - i].UpdateHiddenDelta(Layers[Layers.Length - (i - 1)]);
            }
            var PreLastLayer = Layers[0];
            for (int i = 0; i < InputNeyronsCount; i++)
            {
                var Sinapses = PreLastLayer.GetOutputSinapses(i);
                foreach(var sinapse in Sinapses)
                {
                    sinapse.UpdateWeight(data.Input[i]);
                }
            }
        }
        public NeyralNetwork(NeyralNetworkConfig config, int[] neyronsOnOneLayer)
        {
            Config = config;
            Layers = new Layer[neyronsOnOneLayer.Length - 1];
            InputNeyronsCount = neyronsOnOneLayer[0];
            for (int i = 0; i < Capacity; i++)
            {
                Layers[i] = new Layer(config, neyronsOnOneLayer[i + 1], neyronsOnOneLayer[i]);
            }
        }
    }
}
