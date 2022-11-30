namespace ObandoBryan_Examen.Entidades
{
    public class parent
    {
        public string Object { get; set; }
        public double confidence { get; set; }
        public parent Parent { get; set; }
    }
}
