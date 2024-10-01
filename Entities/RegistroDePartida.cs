namespace backend.Entities
{
    public class RegistroDePartida
    {
        public int Id { get; set; }
        public int CadastroId { get; set; }
        public int qntPartida { get; set; }
        public int qntVitoria { get; set; }
        public int qntDerrota { get; set; }
        public int qntEmpate { get; set; }
        public Cadastro Cadastro { get; set; }
    }
}