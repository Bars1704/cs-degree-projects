using System.Linq;

namespace MyNeyralNetwork
{
    class Layer
    {
        public Neyron[] Neyrons { get; set; }
        public int Capacity { get { return Neyrons.Length; } }
        public double[] Signals
        {
            get
            {
                return Neyrons.Select(x => x.Value).ToArray();
            }
        }
        public override string ToString()
        {
            string result = "Neyron values: ";
            var signals = Signals;
            for (int i = 0; i < signals.Length; i++)
            {
                result += string.Format("{0:N2} ", signals[i]);
            }
            return result;
        }
        public Sinaps[] GetOutputSinapses(int neyronIndex)
        {
            Sinaps[] result = new Sinaps[Capacity];
            for (int i = 0; i < Capacity; i++)
            {
                result[i] = Neyrons[i].InputSinapses[neyronIndex];
            }
            return result;
        }
        public void UpdateOutputDelta(double[] ideal)
        {
            for (int i = 0; i < ideal.Length; i++)
            {
                Neyrons[i].OutputDelta(ideal[i]);
            }
        }
        public void UpdateHiddenDelta(Layer next)
        {
            for (int i = 0; i < Neyrons.Length; i++)
            {
                Neyrons[i].HiddenDelta(next, i);
            }
        }
        public void UpdateValues(double[] previousLayerValues)
        {
            foreach (var neyron in Neyrons)
            {
                neyron.UpdateSignal(previousLayerValues);
            }
        }
        public Neyron this[int index]
        {
            get
            {
                return Neyrons[index];
            }
            set
            {
                Neyrons[index] = value;
            }
        }
        public Layer(NeyralNetworkConfig config, int neyronsCount, int connectionsCount = 1)
        {
            Neyrons = new Neyron[neyronsCount];
            for (int i = 0; i < neyronsCount; i++)
            {
                Neyrons[i] = new Neyron(connectionsCount, config);
            }
        }
    }
}
