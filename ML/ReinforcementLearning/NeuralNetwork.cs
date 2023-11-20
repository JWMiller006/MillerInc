using MillerInc.Convert.Classes;
using MillerInc.Convert.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillerInc.ML.ReinforcementLearning
{
    [Serializable]
    public class NeuralNetwork : Loadable
    {
        #region Init & Copy Functions

        /// <summary>
        /// Creates a new neural network w/o errors, structure is automatically set to 1, 1, 1
        /// </summary>
        public NeuralNetwork()
        {
            IReadOnlyList<int> structure = new List<int>() { 1, 1, 1 };
            values = new float[structure.Count][];
            desiredValues = new float[structure.Count][];
            biases = new float[structure.Count][];
            biasesSmudge = new float[structure.Count][];
            weights = new float[structure.Count - 1][][];
            weightsSmudge = new float[structure.Count - 1][][];

            for (var i = 0; i < structure.Count; i++)
            {
                values[i] = new float[structure[i]];
                desiredValues[i] = new float[structure[i]];
                biases[i] = new float[structure[i]];
                biasesSmudge[i] = new float[structure[i]];
            }

            for (var i = 0; i < structure.Count - 1; i++)
            {
                weights[i] = new float[values[i + 1].Length][];
                weightsSmudge[i] = new float[values[i + 1].Length][];
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] = new float[values[i].Length];
                    weightsSmudge[i][j] = new float[values[i].Length];
                    // After initializing the sizes of all the lists, the weights 
                    //  have to all be randomly initialized with values
                    for (var k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (float)Rand.NextDouble() * (float)Math.Sqrt(2f / weights[i][j].Length);
                    }
                }

            }
        }

        /// <summary>
        /// Initializes the neural network based off of the given structure
        /// </summary>
        /// <param name="structure">the structure of the network in the following format: 
        /// { # of inputLayer nodes, # of hidden layer nodes, (can repeat previous for 
        /// how many desired layers), # of output layer nodes } </param>
        public NeuralNetwork(IReadOnlyList<int> structure)
        {
            values = new float[structure.Count][];
            desiredValues = new float[structure.Count][];
            biases = new float[structure.Count][];
            biasesSmudge = new float[structure.Count][];
            weights = new float[structure.Count - 1][][];
            weightsSmudge = new float[structure.Count - 1][][];

            for (var i = 0; i < structure.Count; i++)
            {
                values[i] = new float[structure[i]];
                desiredValues[i] = new float[structure[i]];
                biases[i] = new float[structure[i]];
                biasesSmudge[i] = new float[structure[i]];
            }

            for (var i = 0; i < structure.Count - 1; i++)
            {
                weights[i] = new float[values[i + 1].Length][];
                weightsSmudge[i] = new float[values[i + 1].Length][];
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] = new float[values[i].Length];
                    weightsSmudge[i][j] = new float[values[i].Length];
                    // After initializing the sizes of all the lists, the weights 
                    //  have to all be randomly initialized with values
                    for (var k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (float)Rand.NextDouble() * (float)Math.Sqrt(2f / weights[i][j].Length);
                    }
                }

            }
        }

        public int[] GetStructure()
        {
            int[] structure = new int[values.Length];
            int i = 0;
            foreach (float[] ints in values)
            {
                structure[i] = ints.Length;
                i++;
            }
            return structure;
        }

        /// <summary>
        /// Init a new network with the given json file
        /// </summary>
        /// <param name="filePath">path to json file *MUST BE IN CORRECT FORMAT*</param>
        public NeuralNetwork(string filePath)
        {
            IReadOnlyList<int> structure = new List<int>() { 1, 1, 1 };
            values = new float[structure.Count][];
            desiredValues = new float[structure.Count][];
            biases = new float[structure.Count][];
            biasesSmudge = new float[structure.Count][];
            weights = new float[structure.Count - 1][][];
            weightsSmudge = new float[structure.Count - 1][][];

            for (var i = 0; i < structure.Count; i++)
            {
                values[i] = new float[structure[i]];
                desiredValues[i] = new float[structure[i]];
                biases[i] = new float[structure[i]];
                biasesSmudge[i] = new float[structure[i]];
            }

            for (var i = 0; i < structure.Count - 1; i++)
            {
                weights[i] = new float[values[i + 1].Length][];
                weightsSmudge[i] = new float[values[i + 1].Length][];
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] = new float[values[i].Length];
                    weightsSmudge[i][j] = new float[values[i].Length];
                    // After initializing the sizes of all the lists, the weights 
                    //  have to all be randomly initialized with values
                    for (var k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (float)Rand.NextDouble() * (float)Math.Sqrt(2f / weights[i][j].Length);
                    }
                }

            }
            LoadFrom(filePath);
        }

        public void LoadFrom(string filePath)
        {
            NeuralNetwork temp = JSON_Converter.Deserialize<NeuralNetwork>(filePath);
            this.CopyFrom(temp);
        }

        public void CopyFrom(NeuralNetwork temp)
        {
            this.values = temp.values;
            this.biases = temp.biases;
            this.weights = temp.weights;
            this.biasesSmudge = temp.biasesSmudge;
            this.weightsSmudge = temp.weightsSmudge;
            this.desiredValues = temp.desiredValues;
            this.LearningRate = temp.LearningRate;
            this.WeightDecay = temp.WeightDecay;
        }
        #endregion

        #region Init Variables

        // Dimensions: Layer, Node #
        public float[][] values;
        public float[][] biases;
        // Dimensions: Layer, Node #, Connected from 
        public float[][][] weights;

        // Dimensions: Layer, Node #
        public float[][] desiredValues;
        public float[][] biasesSmudge;
        // Dimensions: Layer, Node #, Connected from 
        public float[][][] weightsSmudge;

        // HyperParameters 
        // Limits: 0 < WeightDecay < 1;
        // Slowly decreasing the weights
        public float WeightDecay = 0.001f;
        // Limits: 0 <= LearningRate <= 1; (Can make it dynamic)
        public float LearningRate = 0.75f;

        // Random variable to assign values 
        private static readonly System.Random Rand = new();

        #endregion

        #region Main Functions
        /// <summary>
        /// This is supposed to be a function that makes an inference what the output will
        /// be based off of what its current biases and weights are
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>Values of the output nodes</returns>
        public float[] Test(float[] inputs)
        {
            // Set inputs 
            for (int i = 0; i < values[0].Length; i++)
            {
                values[0][i] = inputs[i];
            }

            // Iterate over every node and calculate each value
            for (int i = 1; i < values.Length; i++)
            {
                for (var j = 0; j < values[i].Length; j++)
                {
                    values[i][j] = Sigmoid(x: Sum(values[i - 1], weights[i - 1][j]) + biases[i][j]);
                    desiredValues[i][j] = values[i][j];
                }
            }

            // Return output 
            return values[values.Length - 1];


        }

        // Uses training data over time to make the current predictions more accurate to the real thing
        /// <summary>
        /// This function is supposed to take the outputs that relate to the inputs
        /// and change the weights and biases to match the state of the environment
        /// </summary>
        /// <param name="trainingInputs">This is supposed to be a list of all 
        /// the values of the input nodes that go with the outputs </param>
        /// <param name="trainingOutputs">This is supposed to be a list of all
        /// the values of the output nodes that go with the correlating inputs
        /// </param>
        public void Train(float[][] trainingInputs, float[][] trainingOutputs)
        {
            // Iterate over every training input 
            for (int i = 0; i < trainingInputs.Length; i++)
            {
                // Get outputs from current state of the network
                Test(trainingInputs[i]);

                // Set the actual values of the network 
                for (int j = 0; j < desiredValues[desiredValues.Length - 1].Length; j++)
                {
                    desiredValues[desiredValues.Length - 1][j] = trainingOutputs[i][j];
                }

                // Iterate through the layers backwards
                for (var j = values.Length - 1; j >= 1; j--)
                {
                    for (int k = 0; k < values[j].Length; k++)
                    {
                        // Finds how much the bias needs to change   
                        float biasSmudge = SigmoidDeriviative(values[j][k]) *
                            (desiredValues[j][k] - values[j][k]);

                        biasesSmudge[j][k] += biasSmudge;

                        for (int l = 0; l < values[j - 1].Length; l++)
                        {
                            float weightSmudge = values[j - 1][l] * biasSmudge;
                            weightsSmudge[j - 1][k][l] += weightSmudge;
                            float valueSmudge = weights[j - 1][k][l] * biasSmudge;
                            desiredValues[j - 1][l] += valueSmudge;
                        }
                    }
                }
            }

            // Apply the desired changes to the network and reset the change factor storage (smudge)
            for (var i = values.Length - 1; i >= 1; i--)
            {
                for (var j = 0; j < values[i].Length; j++)
                {
                    biases[i][j] += biasesSmudge[i][j] * LearningRate;
                    biases[i][j] *= 1 - WeightDecay;
                    biasesSmudge[i][j] = 0;

                    for (var k = 0; k < values[i - 1].Length; k++)
                    {
                        weights[i - 1][j][k] += weightsSmudge[i - 1][j][k] * LearningRate;
                        weights[i - 1][j][k] *= 1 - WeightDecay;
                        weightsSmudge[i - 1][j][k] = 0;
                    }

                    desiredValues[i][j] = 0;
                }
            }
        }

        #endregion

        #region Static Functions
        private static float Sum(IEnumerable<float> values, IReadOnlyList<float> weights) =>
            values.Select((v, i) => weights[i]).Sum(); // v1*w1 + v2*w2...

        // Normal Sigmoid Function, but very resource heavy 
        private static float Sigmoid(float x) => 1f / (1f + (float)Math.Exp(-x));

        // Less accurate Sigmoid Function, but less resource intensive, use if computer can't handle  
        public static float HardSigmoid(float x)
        {
            if (x < -2.5f)
                return 0;
            if (x > 2.5f)
                return 1;
            return 0.2f * x + 0.5f;
        }

        // Derivative of the Sigmoid Functioin 
        private static float SigmoidDeriviative(float x) => x * (1 - x);

        #endregion

    }

}
