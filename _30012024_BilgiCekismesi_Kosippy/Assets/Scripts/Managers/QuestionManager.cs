using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public List<Question> GameQuestions = new List<Question>();
    public Question CurrentQuestion;

    private int GameModeLowQuestionPercentage;
    private int GameModeMediumQuestionPercentage;
    private int GameModeHighQuestionPercentage;
    public static QuestionManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    private void Start()
    {

    }
    public void CreateQuestions()
    {
        GameQuestions.Clear();
        Question question1 = new Question(1,"Dünya'nın en büyük memelisi nedir?", new List<Answer>()
        {
            new Answer(0,"Dinazdor",AnswerType.A),
            new Answer(1,"Fil",AnswerType.B),
            new Answer(2,"Yunus Balığı",AnswerType.C),
            new Answer(3,"Balina",AnswerType.D, true),
        },QuestionType.Easy, 60);
        Question question2 = new Question(2, "Dünya üzerinde kaç kıta bulunmaktadır?", new List<Answer>()
        {
            new Answer(0,"5",AnswerType.A),
            new Answer(1,"6",AnswerType.B),
            new Answer(2,"7",AnswerType.C,true),
            new Answer(3,"8",AnswerType.D),
        }, QuestionType.Medium, 85);
        Question question3 = new Question(3, "Hangi gezegen Güneş Sistemi'nde \"Kırmızı Gezegen\" olarak bilinir?", new List<Answer>()
        {
            new Answer(0,"Venüs", AnswerType.A),
            new Answer(1,"Mars",AnswerType.B,true),
            new Answer(2,"Jüpiter", AnswerType.C),
            new Answer(3,"Uranüs", AnswerType.D),
        }, QuestionType.Hard, 80);
        Question question4 = new Question(4, "\"İpek Yolu\" tarihi boyunca hangi iki kıtayı birbirine bağlamıştır?", new List<Answer>()
        {
            new Answer(0,"Asya ve Afrika",AnswerType.A),
            new Answer(1,"Asya ve Avrupa",AnswerType.B,true),
            new Answer(2,"Asya ve Okyanusya",AnswerType.C),
            new Answer(3,"Avrupa ve Afrika", AnswerType.D),
        }, QuestionType.Hard, 150);
        Question question5 = new Question(5, "Dünya'nın en yüksek dağı hangisidir?", new List<Answer>()
        {
            new Answer(0,"Everest Dağı", AnswerType.A, true),
            new Answer(1,"Denali",AnswerType.B),
            new Answer(2,"K2 (Ciuşen İkinci)", AnswerType.C),
            new Answer(3,"Kilimanjaro", AnswerType.D),
        }, QuestionType.Medium, 70);
        Question question6 = new Question(6, "Hangi gezegen, \"Akşam Yıldızı\" veya \"Sabah Yıldızı\" olarak da bilinir?", new List<Answer>()
        {
            new Answer(0,"Mars", AnswerType.A),
            new Answer(1,"Venüs",AnswerType.B, true),
            new Answer(2,"Jüpiter", AnswerType.C),
            new Answer(3,"Satürn", AnswerType.D),
        }, QuestionType.Medium, 70);
        Question question7 = new Question(7, "Hangi gezegen, Güneş Sistemi'nde en büyük olanıdır?", new List<Answer>()
        {
            new Answer(0, "Merkür", AnswerType.A),
            new Answer(1, "Venüs", AnswerType.B),
            new Answer(2, "Dünya", AnswerType.C),
            new Answer(3, "Jüpiter", AnswerType.D, true),
        }, QuestionType.Hard, 90);

        Question question8 = new Question(8, "Türkiye'nin başkenti neresidir?", new List<Answer>()
        {
            new Answer(0, "Ankara", AnswerType.A, true),
            new Answer(1, "İstanbul", AnswerType.B),
            new Answer(2, "İzmir", AnswerType.C),
            new Answer(3, "Antalya", AnswerType.D),
        }, QuestionType.Easy, 45);

        Question question9 = new Question(9, "Hangi yıl Türkiye Cumhuriyeti kurulmuştur?", new List<Answer>()
        {
            new Answer(0, "1920", AnswerType.A),
            new Answer(1, "1921", AnswerType.B),
            new Answer(2, "1922", AnswerType.C),
            new Answer(3, "1923", AnswerType.D, true),
        }, QuestionType.Medium, 60);

        Question question10 = new Question(10, "Einstein'ın ünlü formülü nedir?", new List<Answer>()
        {
            new Answer(0, "E=mc^2", AnswerType.A, true),
            new Answer(1, "F=ma", AnswerType.B),
            new Answer(2, "H2O", AnswerType.C),
            new Answer(3, "πr^2", AnswerType.D),
        }, QuestionType.Easy, 50);

        Question question11 = new Question(11, "Hangi gezegen, adını bir tanrıçadan almıştır?", new List<Answer>()
        {
            new Answer(0, "Mars", AnswerType.A),
            new Answer(1, "Jüpiter", AnswerType.B),
            new Answer(2, "Venüs", AnswerType.C, true),
            new Answer(3, "Satürn", AnswerType.D),
        }, QuestionType.Medium, 65);

        Question question12 = new Question(12, "İnsan vücudundaki en büyük organ hangisidir?", new List<Answer>()
        {
            new Answer(0, "Kalp", AnswerType.A),
            new Answer(1, "Akciğer", AnswerType.B),
            new Answer(2, "Beyin", AnswerType.C),
            new Answer(3, "Cilt", AnswerType.D, true),
        }, QuestionType.Easy, 55);

        Question question13 = new Question(13, "Hangi ülke Güney Amerika kıtasında yer almaz?", new List<Answer>()
        {
            new Answer(0, "Brezilya", AnswerType.A),
            new Answer(1, "Arjantin", AnswerType.B),
            new Answer(2, "Şili", AnswerType.C),
            new Answer(3, "Mısır", AnswerType.D, true),
        }, QuestionType.Medium, 75);

        Question question14 = new Question(14, "Hangi renk gökkuşağının en altında yer almaz?", new List<Answer>()
        {
            new Answer(0, "Kırmızı", AnswerType.A),
            new Answer(1, "Mavi", AnswerType.B),
            new Answer(2, "Yeşil", AnswerType.C, true),
            new Answer(3, "Mor", AnswerType.D),
        }, QuestionType.Easy, 40);

        Question question15 = new Question(15, "Hangi element periyodik tabloda 'Fe' sembolü ile gösterilir?", new List<Answer>()
        {
            new Answer(0, "Fosfor", AnswerType.A),
            new Answer(1, "Demir", AnswerType.B, true),
            new Answer(2, "Flor", AnswerType.C),
            new Answer(3, "Fransiyum", AnswerType.D),
        }, QuestionType.Medium, 70);
        Question question16 = new Question(16, "Hangi gezegen, 'Sessiz Gezegen' olarak bilinir?", new List<Answer>()
        {
            new Answer(0, "Venüs", AnswerType.A),
            new Answer(1, "Merkür", AnswerType.B),
            new Answer(2, "Neptün", AnswerType.C, true),
            new Answer(3, "Uranüs", AnswerType.D),
        }, QuestionType.Hard, 80);

        Question question17 = new Question(17, "Hangi hayvanın evrimleşirken kuş türlerine dönüştüğü düşünülmektedir?", new List<Answer>()
        {
            new Answer(0, "Kaplumbağa", AnswerType.A),
            new Answer(1, "Balina", AnswerType.B),
            new Answer(2, "Timsah", AnswerType.C),
            new Answer(3, "Dinozor", AnswerType.D, true),
        }, QuestionType.Medium, 65);

        Question question18 = new Question(18, "Hangi şehir, \"Beyaz At\" lakabıyla anılmaktadır?", new List<Answer>()
        {
            new Answer(0, "İstanbul", AnswerType.A),
            new Answer(1, "Atina", AnswerType.B, true),
            new Answer(2, "Roma", AnswerType.C),
            new Answer(3, "Paris", AnswerType.D),
        }, QuestionType.Easy, 50);

        Question question19 = new Question(19, "Hangi ünlü ressam, 'Yıldızlı Gece' adlı eseriyle tanınır?", new List<Answer>()
        {
            new Answer(0, "Vincent van Gogh", AnswerType.A, true),
            new Answer(1, "Leonardo da Vinci", AnswerType.B),
            new Answer(2, "Pablo Picasso", AnswerType.C),
            new Answer(3, "Claude Monet", AnswerType.D),
        }, QuestionType.Hard, 90);

        Question question20 = new Question(20, "Hangi ülkenin bayrağında ay ve yıldız bulunur?", new List<Answer>()
        {
            new Answer(0, "Türkiye", AnswerType.A, true),
            new Answer(1, "İtalya", AnswerType.B),
            new Answer(2, "Brezilya", AnswerType.C),
            new Answer(3, "Japonya", AnswerType.D),
        }, QuestionType.Easy, 45);

        Question question21 = new Question(21, "Hangi gezegen, adını bir tanrıçadan almıştır?", new List<Answer>()
        {
            new Answer(0, "Mars", AnswerType.A),
            new Answer(1, "Jüpiter", AnswerType.B),
            new Answer(2, "Venüs", AnswerType.C, true),
            new Answer(3, "Satürn", AnswerType.D),
        }, QuestionType.Medium, 65);

        Question question22 = new Question(22, "İnsan vücudundaki en büyük organ hangisidir?", new List<Answer>()
        {
            new Answer(0, "Kalp", AnswerType.A),
            new Answer(1, "Akciğer", AnswerType.B),
            new Answer(2, "Beyin", AnswerType.C),
            new Answer(3, "Cilt", AnswerType.D, true),
        }, QuestionType.Easy, 55);

        Question question23 = new Question(23, "Hangi ülke Güney Amerika kıtasında yer almaz?", new List<Answer>()
        {
            new Answer(0, "Brezilya", AnswerType.A),
            new Answer(1, "Arjantin", AnswerType.B),
            new Answer(2, "Şili", AnswerType.C),
            new Answer(3, "Mısır", AnswerType.D, true),
        }, QuestionType.Medium, 75);

        Question question24 = new Question(24, "Hangi renk gökkuşağının en altında yer almaz?", new List<Answer>()
        {
            new Answer(0, "Kırmızı", AnswerType.A),
            new Answer(1, "Mavi", AnswerType.B),
            new Answer(2, "Yeşil", AnswerType.C, true),
            new Answer(3, "Mor", AnswerType.D),
        }, QuestionType.Easy, 40);

        Question question25 = new Question(25, "Hangi element periyodik tabloda 'Fe' sembolü ile gösterilir?", new List<Answer>()
        {
            new Answer(0, "Fosfor", AnswerType.A),
            new Answer(1, "Demir", AnswerType.B, true),
            new Answer(2, "Flor", AnswerType.C),
            new Answer(3, "Fransiyum", AnswerType.D),
        }, QuestionType.Medium, 70);
        Question question26 = new Question(26, "Hangi gezegen, 'Akşam Yıldızı' olarak bilinir?", new List<Answer>()
        {
            new Answer(0, "Mars", AnswerType.A),
            new Answer(1, "Jüpiter", AnswerType.B),
            new Answer(2, "Venüs", AnswerType.C, true),
            new Answer(3, "Satürn", AnswerType.D),
        }, QuestionType.Medium, 70);

        Question question27 = new Question(27, "Hangi ünlü bilim adamı, 'E=mc^2' formülüyle tanınır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B, true),
            new Answer(2, "Galileo Galilei", AnswerType.C),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Hard, 95);

        Question question28 = new Question(28, "Hangi ülke, 'Hindistan' adını taşır?", new List<Answer>()
        {
            new Answer(0, "Tayland", AnswerType.A),
            new Answer(1, "Bangladeş", AnswerType.B),
            new Answer(2, "Pakistan", AnswerType.C),
            new Answer(3, "Hindistan", AnswerType.D, true),
        }, QuestionType.Medium, 80);

        Question question29 = new Question(29, "Hangi element periyodik tabloda 'O' sembolü ile gösterilir?", new List<Answer>()
        {
            new Answer(0, "Oksijen", AnswerType.A, true),
            new Answer(1, "Gümüş", AnswerType.B),
            new Answer(2, "Amonyak", AnswerType.C),
            new Answer(3, "Sodyum", AnswerType.D),
        }, QuestionType.Hard, 85);

        Question question30 = new Question(30, "Hangi ünlü yazar, 'Suç ve Ceza' adlı eserin yazarıdır?", new List<Answer>()
        {
            new Answer(0, "Fyodor Dostoyevsky", AnswerType.A, true),
            new Answer(1, "Leo Tolstoy", AnswerType.B),
            new Answer(2, "Charles Dickens", AnswerType.C),
            new Answer(3, "Jane Austen", AnswerType.D),
        }, QuestionType.Easy, 60);

        Question question31 = new Question(31, "Hangi gezegen, 'Gece Gökyüzü'nün en parlak gezegenidir?", new List<Answer>()
        {
            new Answer(0, "Merkür", AnswerType.A),
            new Answer(1, "Jüpiter", AnswerType.B, true),
            new Answer(2, "Satürn", AnswerType.C),
            new Answer(3, "Neptün", AnswerType.D),
        }, QuestionType.Medium, 75);

        Question question32 = new Question(32, "Hangi ünlü ressam, 'Mona Lisa' tablosunun yaratıcısıdır?", new List<Answer>()
        {
            new Answer(0, "Vincent van Gogh", AnswerType.A),
            new Answer(1, "Leonardo da Vinci", AnswerType.B, true),
            new Answer(2, "Pablo Picasso", AnswerType.C),
            new Answer(3, "Claude Monet", AnswerType.D),
        }, QuestionType.Hard, 90);

        Question question33 = new Question(33, "Hangi ünlü fizikçi, 'Gravitasyon Yasası'yla bilinir?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A, true),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Galileo Galilei", AnswerType.C),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Medium, 70);

        Question question34 = new Question(34, "Hangi ülkenin bayrağında ay ve yıldız bulunur?", new List<Answer>()
        {
            new Answer(0, "Türkiye", AnswerType.A, true),
            new Answer(1, "İtalya", AnswerType.B),
            new Answer(2, "Brezilya", AnswerType.C),
            new Answer(3, "Japonya", AnswerType.D),
        }, QuestionType.Easy, 45);

        Question question35 = new Question(35, "Hangi ünlü bilim adamı, 'Evrenin Genişlediği' teorisini ortaya atmıştır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Edwin Hubble", AnswerType.C, true),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Hard, 85);
        Question question36 = new Question(36, "Hangi ünlü ressam, 'Gülümseyen Mona Lisa' tablosunun yaratıcısıdır?", new List<Answer>()
        {
            new Answer(0, "Vincent van Gogh", AnswerType.A),
            new Answer(1, "Leonardo da Vinci", AnswerType.B, true),
            new Answer(2, "Pablo Picasso", AnswerType.C),
            new Answer(3, "Claude Monet", AnswerType.D),
        }, QuestionType.Hard, 95);

        Question question37 = new Question(37, "Hangi ünlü fizikçi, 'Teorik Fizikteki Temel Kurallar' kitabının yazarıdır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Richard Feynman", AnswerType.C, true),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Hard, 90);

        Question question38 = new Question(38, "Hangi ülke, 'Amazon Ormanları' ile öne çıkar?", new List<Answer>()
        {
            new Answer(0, "Brezilya", AnswerType.A, true),
            new Answer(1, "Endonezya", AnswerType.B),
            new Answer(2, "Kolombiya", AnswerType.C),
            new Answer(3, "Gana", AnswerType.D),
        }, QuestionType.Medium, 80);

        Question question39 = new Question(39, "Hangi gezegen, 'Ring Nebula' olarak bilinen bir nebula ile ünlüdür?", new List<Answer>()
        {
            new Answer(0, "Satürn", AnswerType.A),
            new Answer(1, "Jüpiter", AnswerType.B),
            new Answer(2, "Mars", AnswerType.C),
            new Answer(3, "Uranüs", AnswerType.D, true),
        }, QuestionType.Hard, 85);

        Question question40 = new Question(40, "Hangi ünlü filozof, 'Varlık ve Hiçlik' konularını ele almıştır?", new List<Answer>()
        {
            new Answer(0, "Aristoteles", AnswerType.A),
            new Answer(1, "Platon", AnswerType.B),
            new Answer(2, "Martin Heidegger", AnswerType.C, true),
            new Answer(3, "Jean-Jacques Rousseau", AnswerType.D),
        }, QuestionType.Easy, 60);

        Question question41 = new Question(41, "Hangi ünlü bilim adamı, 'Işığın Hızı' üzerine önemli çalışmalara imza atmıştır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Galileo Galilei", AnswerType.C, true),
            new Answer(3, "Niels Bohr", AnswerType.D),
        }, QuestionType.Medium, 75);

        Question question42 = new Question(42, "Hangi ünlü yazar, 'Suç ve Ceza' adlı eserin yazarıdır?", new List<Answer>()
        {
            new Answer(0, "Fyodor Dostoyevsky", AnswerType.A, true),
            new Answer(1, "Leo Tolstoy", AnswerType.B),
            new Answer(2, "Charles Dickens", AnswerType.C),
            new Answer(3, "Jane Austen", AnswerType.D),
        }, QuestionType.Easy, 60);

        Question question43 = new Question(43, "Hangi ünlü bilim adamı, 'Zamanın Doğası' üzerine teoriler geliştirmiştir?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Stephen Hawking", AnswerType.C, true),
            new Answer(3, "Niels Bohr", AnswerType.D),
        }, QuestionType.Hard, 85);

        Question question44 = new Question(44, "Hangi ülkenin bayrağında ay ve yıldız bulunur?", new List<Answer>()
        {
            new Answer(0, "Türkiye", AnswerType.A, true),
            new Answer(1, "İtalya", AnswerType.B),
            new Answer(2, "Brezilya", AnswerType.C),
            new Answer(3, "Japonya", AnswerType.D),
        }, QuestionType.Easy, 45);

        Question question45 = new Question(45, "Hangi ünlü fizikçi, 'Evrenin Genişlediği' teorisini ortaya atmıştır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Edwin Hubble", AnswerType.C, true),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Hard, 85);
        Question question46 = new Question(46, "Hangi ünlü bilim adamı, 'Schrodinger'in Kedisi' düşünce deneyi ile tanınır?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Erwin Schrödinger", AnswerType.C, true),
            new Answer(3, "Niels Bohr", AnswerType.D),
        }, QuestionType.Hard, 85);

        Question question47 = new Question(47, "Hangi ülkenin bayrağında sadece tek bir renk bulunur?", new List<Answer>()
        {
            new Answer(0, "Libya", AnswerType.A, true),
            new Answer(1, "Monako", AnswerType.B),
            new Answer(2, "Endonezya", AnswerType.C),
            new Answer(3, "Kırgızistan", AnswerType.D),
        }, QuestionType.Easy, 50);

        Question question48 = new Question(48, "Hangi ünlü bilim adamı, 'Yerçekimi Elması' teorisini geliştirmiştir?", new List<Answer>()
        {
            new Answer(0, "Isaac Newton", AnswerType.A),
            new Answer(1, "Albert Einstein", AnswerType.B),
            new Answer(2, "Galileo Galilei", AnswerType.C, true),
            new Answer(3, "Stephen Hawking", AnswerType.D),
        }, QuestionType.Medium, 70);

        Question question49 = new Question(49, "Hangi ünlü ressam, 'Gece Gözcüsü' tablosunun yaratıcısıdır?", new List<Answer>()
        {
            new Answer(0, "Vincent van Gogh", AnswerType.A),
            new Answer(1, "Leonardo da Vinci", AnswerType.B),
            new Answer(2, "Rembrandt", AnswerType.C, true),
            new Answer(3, "Claude Monet", AnswerType.D),
        }, QuestionType.Hard, 90);

        Question question50 = new Question(50, "Hangi gezegen, \"Dünya'dan\" en çok benzerlik gösteren atmosfere sahiptir?", new List<Answer>()
        {
            new Answer(0, "Mars", AnswerType.A),
            new Answer(1, "Venüs", AnswerType.B, true),
            new Answer(2, "Jüpiter", AnswerType.C),
            new Answer(3, "Satürn", AnswerType.D),
        }, QuestionType.Medium, 75);
        List<Question> questions = new List<Question>();

        questions.Add(question1);
        questions.Add(question2);
        questions.Add(question3);
        questions.Add(question4);
        questions.Add(question5);
        questions.Add(question7);
        questions.Add(question8);
        questions.Add(question9);
        questions.Add(question10);
        questions.Add(question11);
        questions.Add(question12);
        questions.Add(question13);
        questions.Add(question14);
        questions.Add(question15);
        questions.Add(question16);
        questions.Add(question17);
        questions.Add(question18);
        questions.Add(question19);
        questions.Add(question20);
        questions.Add(question21);
        questions.Add(question22);
        questions.Add(question23);
        questions.Add(question24);
        questions.Add(question25);
        questions.Add(question36);
        questions.Add(question37);
        questions.Add(question38);
        questions.Add(question39);
        questions.Add(question40);
        questions.Add(question41);
        questions.Add(question42);
        questions.Add(question43);
        questions.Add(question44);
        questions.Add(question45);
        questions.Add(question46);
        questions.Add(question47);
        questions.Add(question48);
        questions.Add(question49);
        questions.Add(question50);

        GameModeTypeForQuestionsPercentage(GameManager.instance.GetGameDiff());
        GameModeTypeForQuestions(questions);
    }
    public bool IsCurrentAnswerOfQuestionCorrect(Answer _currentAnswer)
    {
        if (_currentAnswer != null){ if (_currentAnswer.isTrue) {return true;} else {return false;} }
        else { Debug.Log("Cevaba ulasilamadi."); return false; }
    }
    public void SetCurrentQuestionBeRandom()
    {
        int index = Random.Range(0, GameQuestions.Count);
        if (GameQuestions.Count > 0)
        {
            CurrentQuestion = GameQuestions[index];
            // bu kisimda, durduktan sonra 2'serli devam ediyor sure.
            QuestionPanelController.instance.PlayNextQuestionSoundEffect();
            QuestionPanelController.instance.FlagToggleIsOn();
            QuestionPanelController.instance.SetQuestionPanelUIS(CurrentQuestion);
            UIManager.instance.AIPassQuestionObjActivation(false);
            SetAnswerTime();
            GameQuestions.RemoveAt(index);
        }
        else
        {
            GameManager.instance.EndTheGame(GameEndingType.QuestionEnding);
        }
        // sorular bittiginde oyunda bitsin...
    }
    public void GameModeTypeForQuestionsPercentage(DifficultyType _GameDiff)
    {        
        switch (_GameDiff)
        {
            case DifficultyType.None:
                break;
            case DifficultyType.Low:
                GameModeLowQuestionPercentage = 80;
                GameModeMediumQuestionPercentage = 40;
                GameModeHighQuestionPercentage = 10;
                break;
            case DifficultyType.Medium:
                GameModeLowQuestionPercentage = 50;
                GameModeMediumQuestionPercentage = 80;
                GameModeHighQuestionPercentage = 30;
                break;
            case DifficultyType.High:
                GameModeLowQuestionPercentage = 20;
                GameModeMediumQuestionPercentage = 50;
                GameModeHighQuestionPercentage = 80;
                break;
            default:
                break;
        }        
    }
    public void GameModeTypeForQuestions(List<Question> _allQuestions)
    {
        List<Question> currentQuestions = new List<Question>();
        //List<Question> garbageQuestions = new List<Question>();
        int EasyQuestionCount = 0, MediumQuestionCount = 0, HardQuestionCount = 0;
        DifficultyType difficulty = GameManager.instance.GetGameDiff();
        int length = _allQuestions.Count;
        for (int i = 0; i < length; i++)
        {
            
            //Question currentQuestion;
            switch (difficulty)
            {
                case DifficultyType.None:
                    break;
                case DifficultyType.Low:
                    int pertange = Random.Range(0, GameModeLowQuestionPercentage + 1);
                    if (pertange <= GameModeHighQuestionPercentage)
                    {
                        HardQuestionCount++;
                    }
                    else if (pertange <= GameModeMediumQuestionPercentage)
                    {
                        MediumQuestionCount++;
                    }
                    else if (pertange <= GameModeLowQuestionPercentage)
                    {
                        EasyQuestionCount++;
                    }
                    break;
                case DifficultyType.Medium:
                    int pertange1 = Random.Range(0, GameModeMediumQuestionPercentage + 1);
                    if (pertange1 <= GameModeHighQuestionPercentage)
                    {
                        HardQuestionCount++;                                              
                    }
                    else if (pertange1 <= GameModeLowQuestionPercentage)
                    {
                        EasyQuestionCount++;                        
                    }
                    else if (pertange1 <= GameModeMediumQuestionPercentage)
                    {
                        MediumQuestionCount++;
                    }
                    break;
                case DifficultyType.High:
                    int pertange2 = Random.Range(0, GameModeHighQuestionPercentage + 1);
                    if (pertange2 <= GameModeLowQuestionPercentage)
                    {
                        EasyQuestionCount++; // 2
                    }
                    else if (pertange2 <= GameModeMediumQuestionPercentage)
                    {
                        MediumQuestionCount++; // 6
                    }
                    else if (pertange2 <= GameModeHighQuestionPercentage)
                    {
                        HardQuestionCount++; // 10
                    }
                    break;
                default:
                    break;
            }
        }
        Debug.Log("Oyun Modu => " + difficulty + " Soru Sayıları = High => " + HardQuestionCount + " Medium => " + MediumQuestionCount + " Low => " + EasyQuestionCount);
        List<Question> EasyQuestions = _allQuestions.Where(x => x.Type == QuestionType.Easy).ToList();
        List<Question> MediumQuestions = _allQuestions.Where(x => x.Type == QuestionType.Medium).ToList();
        List<Question> HardQuestions = _allQuestions.Where(x => x.Type == QuestionType.Hard).ToList();
        Debug.Log("Easy Questions Count => " + EasyQuestions.Count + " Medium Questions Count => " + MediumQuestions.Count + " Hard Questions Count => " + HardQuestions.Count);
        // Diff => High
        // Low => 15, Medium => 15, High => 20
        // Low => 40, Medium => 50, High => 80
        // Low => 2, Medium => 6, High => 10
        if (EasyQuestions.Count < EasyQuestionCount)
        {
            EasyQuestionCount = EasyQuestions.Count;
            Debug.Log("Equal Easy Count => " + EasyQuestionCount + " EasyQuestion.Count => " + EasyQuestions.Count);
        }
        if (MediumQuestions.Count < MediumQuestionCount)
        {
            MediumQuestionCount = MediumQuestions.Count;
            Debug.Log("Equal Medium Count => " + MediumQuestionCount + " MediumQuestions.Count => " + MediumQuestions.Count);
        }
        if (HardQuestions.Count < HardQuestionCount)
        {
            HardQuestionCount = HardQuestions.Count;
            Debug.Log("Equal Hard Count => " + HardQuestionCount + " HardQuestion.Count => " + HardQuestions.Count);
        }
        for (int i = 0; i < EasyQuestionCount; i++)
        {
            if (EasyQuestions.Count > 0)
            {
                int randomIndex = Random.Range(0, EasyQuestions.Count); // 8
                currentQuestions.Add(EasyQuestions[randomIndex]);
                EasyQuestions.RemoveAt(randomIndex);
            }
        }
        for (int i = 0; i < MediumQuestionCount; i++)
        {
            if (MediumQuestions.Count > 0)
            {
                int randomIndex = Random.Range(0, MediumQuestions.Count);
                currentQuestions.Add(MediumQuestions[randomIndex]);
                MediumQuestions.RemoveAt(randomIndex);
            }
        }
        for (int i = 0; i < HardQuestionCount; i++)
        {
            if (HardQuestions.Count > 0)
            {
                int randomIndex = Random.Range(0, HardQuestions.Count);
                currentQuestions.Add(HardQuestions[randomIndex]);
                HardQuestions.RemoveAt(randomIndex);
            }
        }
        // 18 tane soru elde ettik bu ornekte.
        Debug.Log("CurrentQuestions.Count => " + currentQuestions.Count);
        GameQuestions.Clear();
        foreach (Question _question in currentQuestions)
        {
            GameQuestions.Add(_question);
        }
        Debug.Log("GameQuestions.Count => " + GameQuestions.Count);
    }
    public void StopSetAnswerTimeCoroutine()
    {
        TimeManager.instance.StopPlayerAnswerCoroutine();
    }
    public void SetAnswerTime()
    {
        TimeManager.instance.SetPlayerAnswerCoroutine(TimeManager.instance.PlayerAnswerTimeControl(CurrentQuestion.AnswerSecondTime));
    }
}
public enum QuestionType
{
    None,
    Easy,
    Medium,
    Hard
}
