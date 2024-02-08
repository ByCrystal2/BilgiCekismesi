using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionToggleController : MonoBehaviour
{
    Toggle MyToggle;
    [SerializeField] AnswerType ToggleAnswerType;
    private void Awake()
    {
        MyToggle = GetComponent<Toggle>();
    }
    public void ToggleOnValueChangedControl()
    {
        if (MyToggle.isOn)
        {
            List<Answer> answerList = new List<Answer>();
            answerList = QuestionManager.instance.CurrentQuestion.GetMyAnswers();
            foreach (var answer in answerList)
            {
                answer.isSelected = false;
            }
            Answer SelectedAnswer = answerList.Where(x=> x.AnswerType == ToggleAnswerType).SingleOrDefault();
            SelectedAnswer.isSelected = true;            
            QuestionPanelController.instance.ActivationTrueFalsePanel(true);
            TimeManager.instance.StopGameTimeCoroutine();
        }
        else
            QuestionPanelController.instance.ActivationTrueFalsePanel(false);
    }
}
