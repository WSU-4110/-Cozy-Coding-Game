using Xunit;

namespace CozyCove.UnitTests

{
   
    public class TestableDataTypesSceneLoad
    {
        public string LastLoadedScene { get; private set; }

        public void LoadLevel3(string sceneName)
        {
            // Instead of actually loading the scene, we track what WOULD be loaded
            LastLoadedScene = sceneName;
        }
    }

    // Testable version of CastingQuizManager logic
    public class TestableCastingQuizManager
    {
        public string LastFeedback { get; private set; }
        public bool? LastAnswerCorrect { get; private set; }
        public int CurrentQuestionIndex { get; private set; }

        // Mock question data
        private TestableQuestionData[] questions = new TestableQuestionData[]
        {
            new TestableQuestionData { CorrectAnswer = "35" },
            new TestableQuestionData { CorrectAnswer = "35.0" },
            new TestableQuestionData { CorrectAnswer = "35.82" }
        };

        public TestableCastingQuizManager()
        {
            CurrentQuestionIndex = 0;
        }

        public void CheckAnswer(string selectedAnswer)
        {
            if (CurrentQuestionIndex >= questions.Length) return;

            string correctAnswer = questions[CurrentQuestionIndex].CorrectAnswer;

            if (selectedAnswer == correctAnswer)
            {
                LastFeedback = "CORRECT!";
                LastAnswerCorrect = true;
                CurrentQuestionIndex++;
            }
            else
            {
                LastFeedback = "INCORRECT. Remember, int() chops off the decimal, and str() converts anything to text!";
                LastAnswerCorrect = false;
            }
        }

        public bool HasMoreQuestions()
        {
            return CurrentQuestionIndex < questions.Length;
        }
    }

    // Simple data structure for testing
    public class TestableQuestionData
    {
        public string CorrectAnswer { get; set; }
    }

    // ===== TEST CLASSES =====

    public class DataTypesSceneLoadTests
    {
        [Fact]
        public void LoadLevel3_WithValidSceneName_SetsCorrectScene()
        {
            // ARRANGE
            var sceneLoader = new TestableDataTypesSceneLoad();
            string expectedScene = "Level3_DataTypes";

            // ACT
            sceneLoader.LoadLevel3(expectedScene);

            // ASSERT
            Assert.Equal(expectedScene, sceneLoader.LastLoadedScene);
        }

        [Fact]
        public void LoadLevel3_WithEmptySceneName_SetsEmptyScene()
        {
            // ARRANGE
            var sceneLoader = new TestableDataTypesSceneLoad();
            string expectedScene = "";

            // ACT
            sceneLoader.LoadLevel3(expectedScene);

            // ASSERT
            Assert.Equal(expectedScene, sceneLoader.LastLoadedScene);
        }

        [Fact]
        public void LoadLevel3_WithNullSceneName_SetsNullScene()
        {
            // ARRANGE
            var sceneLoader = new TestableDataTypesSceneLoad();
            string expectedScene = null;

            // ACT
            sceneLoader.LoadLevel3(expectedScene);

            // ASSERT
            Assert.Equal(expectedScene, sceneLoader.LastLoadedScene);
        }
    }

    public class CastingQuizManagerTests
    {
        [Fact]
        public void CheckAnswer_CorrectAnswer_AdvancesToNextQuestion()
        {
            // ARRANGE
            var quizManager = new TestableCastingQuizManager();
            string correctAnswer = "35";

            // ACT
            quizManager.CheckAnswer(correctAnswer);

            // ASSERT
            Assert.True(quizManager.LastAnswerCorrect);
            Assert.Equal("CORRECT!", quizManager.LastFeedback);
            Assert.Equal(1, quizManager.CurrentQuestionIndex);
        }

        [Fact]
        public void CheckAnswer_IncorrectAnswer_ShowsFeedbackButNoAdvance()
        {
            // ARRANGE
            var quizManager = new TestableCastingQuizManager();
            string wrongAnswer = "35.88";

            // ACT
            quizManager.CheckAnswer(wrongAnswer);

            // ASSERT
            Assert.False(quizManager.LastAnswerCorrect);
            Assert.Contains("INCORRECT", quizManager.LastFeedback);
            Assert.Equal(0, quizManager.CurrentQuestionIndex);
        }

        [Fact]
        public void CheckAnswer_AllQuestionsCorrect_CompletesQuiz()
        {
            // ARRANGE
            var quizManager = new TestableCastingQuizManager();
            string[] correctAnswers = { "35", "35.0", "35.82" };

            // ACT
            foreach (string answer in correctAnswers)
            {
                quizManager.CheckAnswer(answer);
            }

            // ASSERT
            Assert.False(quizManager.HasMoreQuestions());
            Assert.Equal(3, quizManager.CurrentQuestionIndex);
        }
    }
}