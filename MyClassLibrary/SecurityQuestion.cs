namespace Project4.Models
{
    public class SecurityQuestion
    {
        private int questionID;
        private string questionName;
        private string questionText;

        public SecurityQuestion(int questionID, string questionName, string questionText)
        {
            this.questionID = questionID;
            this.questionName = questionName;
            this.questionText = questionText;
        }

        public SecurityQuestion()
        {

        }

        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        public string QuestionName
        {
            get { return questionName; }
            set { questionName = value; }
        }

        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }
    }
}
