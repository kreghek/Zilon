using JetBrains.Annotations;

namespace Zilon.Core.NameGeneration
{
    [PublicAPI]
    public class IdNameGenerator : IPersonNameGenerator
    {
        private static int _id;

        public string CreateName()
        {
            _id++;
            return _id.ToString();
        }
    }
}
