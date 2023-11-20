namespace AuditApp.Shared.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string Requirement { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;

        private bool _isInCompliance;
        public bool IsInCompliance
        {
            get
            {
                if (_isInCompliance)
                {
                    State = "Atende";
                }
                else
                {
                    State = "Não atende";
                }

                return _isInCompliance;
            }
            set
            {
                if (value)
                {
                    State = "Atende";
                }
                else
                {
                    State = "Não atende";
                }

                _isInCompliance = value;
            }
        }

        public string State { get; set; } = string.Empty;
    }
}
