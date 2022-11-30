using System;

namespace ObandoBryan_Examen.Entidades
{
    public class do_response
    {
        public @object[] objects { get; set; }
        public string requestId { get; set; }
        public metadata metadata { get; set; }
        public string modelVersion { get; set; }
    }
}
