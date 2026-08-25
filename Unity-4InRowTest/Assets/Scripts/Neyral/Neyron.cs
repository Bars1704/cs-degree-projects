using System;
using System.Collections.Generic;

namespace MyNeyralNetwork
{
    class Neyron
    {
        public int Capacity { get { return InputSinapses.Length; } }
        public double Value { get; set; }
        public double Delta { get; set; }
        public Sinaps[] InputSinapses { get; set; }
        public override string ToString()
        {
            string result = "Weights: ";
            foreach (var x in InputSinapses)
            {
                result += string.Format("{0} ", x);
            }
            return result;
        }
        double SigmoidDiffer(double x)
        {
            return (1 - x) * x;
        }
        public void OutputDelta(double Ideal)
        {
            Delta = (Ideal - Value) * SigmoidDiffer(Value);
        }
        public void HiddenDelta(Layer NextLayer, int index)
        {
            double sum = 0;
            for (int i = 0; i < NextLayer.Capacity; i++)
            {
                var currentNeyron = NextLayer.Neyrons[i];
                sum += currentNeyron.Delta * currentNeyron.InputSinapses[index];
            }
            Delta = SigmoidDiffer(Value) * sum;
            var OutputSinapses = NextLayer.GetOutputSinapses(index);
            foreach (var sinaps in OutputSinapses)
            {
                sinaps.UpdateWeight(Value);
            }
        }
        public void UpdateSignal(double[] inputSignals)
        {
            if (InputSinapses.Length != inputSignals.Length)
            {
                throw new Exception("inputWeights.Count != inputSignals.Count");
            }
            Value = 0;
            for (int i = 0; i < Capacity; i++)
            {
                Value += inputSignals[i] * InputSinapses[i];
            }
            Value = NeyralNetwork.Sigmoid(Value);
        }
        public Sinaps this[int index]
        {
            get
            {
                return InputSinapses[index];
            }
            set
            {
                InputSinapses[index] = value;
            }
        }
        public Neyron(int weightsCount, NeyralNetworkConfig config)
        {
            Value = 0;
            Delta = 0;
            InputSinapses = new Sinaps[weightsCount];
            for (int i = 0; i < weightsCount; i++)
            {
                InputSinapses[i] = new Sinaps(config, this);
            }
        }

    }
}
