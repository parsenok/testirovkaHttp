using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using Newtonsoft.Json;
using NUnitLite;
using NUnit.Framework;

public class ProgramTests
{
    private HttpClient httpClient;

    [SetUp]
    public void Setup()
    {
        httpClient = new HttpClient();
    }

    [TearDown]
    public void TearDown()
    {
        httpClient.Dispose();
    }

    [Test]
    [AllureFeature("HTTP")]
    [AllureStory("Successful Request")]
    public async Task HttpClient_RequestSuccess()
    {
        string url = "https://api.sampleapis.com/futurama/questions";
        HttpResponseMessage response = await httpClient.GetAsync(url);
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [Test]
    [AllureFeature("String Processing")]
    [AllureStory("Split JSON Content")]
    public void SplitJsonContentIntoLines()
    {
        string jsonContent = "Question 1\r\nQuestion 2\r\nQuestion 3";
        string[] questions = jsonContent.Split(new[] { "\r\n" }, StringSplitOptions.None);
        Assert.IsNotNull(questions);
        Assert.AreEqual(3, questions.Length);
    }

    [Test]
    [AllureFeature("Console Output")]
    [AllureStory("Print Questions")]
    public void PrintQuestionsToConsole()
    {
        string[] questions = new[] { "Question 1", "Question 2", "Question 3" };
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            foreach (var question in questions)
            {
                Console.WriteLine(question);
            }
            string consoleOutput = sw.ToString();
            Assert.AreEqual("Question 1\r\nQuestion 2\r\nQuestion 3\r\n", consoleOutput);
        }
    }

    [Test]
    [AllureFeature("HTTP")]
    [AllureStory("Failed Request")]
    public async Task HttpClient_RequestFailure()
    {
        string url = "https://nonexistent-url.com"; // URL, который вызовет ошибку
        HttpResponseMessage response = await httpClient.GetAsync(url);
        Assert.IsFalse(response.IsSuccessStatusCode);
    }

    [Test]
    [AllureFeature("Exception Handling")]
    [AllureStory("Handle Exception")]
    public void HandleException()
    {
        Exception exception = new Exception("Test Exception");
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            Console.WriteLine($"Произошла ошибка: {exception.Message}");
            string consoleOutput = sw.ToString();
            Assert.AreEqual("Произошла ошибка: Test Exception\r\n", consoleOutput);
        }
    }
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var testAssembly = typeof(ProgramTests).Assembly;
            var testRunner = new AutoRun(testAssembly);
            Console.WriteLine(testRunner.Execute(args));
        }
    }

}