using System.Collections;
using System.Collections.Generic;
using System.Linq;
using easyar;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TrueOrFalse : MonoBehaviour
{
    public Text questionTextPlace;
    private ImageTrackerFrameFilter _filter;
    private readonly Dictionary<string, bool> _questions = new Dictionary<string, bool>();
    private string _question = "";
    private const float TimeToCheck = 2;
    private float _passedTime;
    private bool _userGiveAnswer;
    private readonly Random _random = new Random();

    private void FillPrime()
    {
        bool[] isNotPrime = new bool[1001];
        isNotPrime[0] = isNotPrime[1] = true;
        const int n = 1000;
        for (int i = 2; i * i <= n; ++i)
        {
            if (!isNotPrime[i])
            {
                for (int j = i * i; j <= n; j+=i)
                {
                    isNotPrime[j] = true;
                }
            }
        }

        for (int i = 0; i <= n; ++i)
        {
            var question = $"{i} is prime number?";
            _questions[question] = !isNotPrime[i];
        }
    }

    private void FillFibonacci()
    {
        var fibonacci = new List<int> {0, 1};
        while (fibonacci.Count <= 15)
        {
            int n = fibonacci.Count;
            fibonacci.Add(fibonacci[n - 1] + fibonacci[n - 2]);
        }

        int last = fibonacci[fibonacci.Count - 1];
        for (int i = 0; i <= last; ++i)
        {
            var question = $"{i} is fibonacci number?";
            _questions[question] = fibonacci.Contains(i);
        }
    }
    
    public void Start()
    {
        _filter = gameObject.GetComponent<ImageTrackerFrameFilter>();
        questionTextPlace.color = Color.yellow;
        FillPrime();
        FillFibonacci();
        if (_filter != null)
        {
            Debug.Log("Success");
        }
    }

    public void Update()
    {
        if (_userGiveAnswer)
        {
            _passedTime += Time.deltaTime;
            if (_passedTime < TimeToCheck)
            {
                return;
            }

            questionTextPlace.text = _questions.Keys.ToList()[_random.Next(_questions.Count)];
            _passedTime = 0;
        }
        
        var answer = _filter!.TargetControllers.Where(target => target.IsTracked).Select(target => target.name)
            .FirstOrDefault();
        if (answer != null)
        {
            bool ans = answer.ToLower() == "correct";
            questionTextPlace.color = _questions[_question] == ans ? Color.green : Color.red;
            _userGiveAnswer = true;
        }
    }
}
