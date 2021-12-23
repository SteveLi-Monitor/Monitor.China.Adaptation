using System.Collections.Generic;

namespace Application.AutoCompletes.Commands.Part
{
    public class PartCommandResp
    {
        public IEnumerable<Part> Parts { get; set; }

        public class Part
        {
            public string Id { get; set; }

            public string PartNumber { get; set; }

            public int Type { get; set; }

            public string Description { get; set; }
        }
    }
}
