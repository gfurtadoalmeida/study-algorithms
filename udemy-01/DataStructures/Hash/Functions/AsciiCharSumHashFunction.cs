namespace DataStructures.Hash.Functions
{
    public sealed class AsciiCharSumHashFunction : IHashFunction
    {
        public static readonly IHashFunction Instance = new AsciiCharSumHashFunction();

        private AsciiCharSumHashFunction()
        {
        }

        public int Hash(string value)
        {
            int sum = 0;

            for (int i = 0; i < value.Length; i++)
            {
                sum += value[i];
            }

            return sum;
        }
    }
}
