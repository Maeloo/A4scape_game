using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnswerWrapper
{

    public AnswerWrapper(string AT, Texture AS, KeyCode AI, string AC)
    {
        AnswerText = AT;
        AnswerSprite = AS;
        AnswerInput = AI;
        AnswerCallback = AC;
    }

    public string AnswerText;
    public Texture AnswerSprite;
    public KeyCode AnswerInput;
    public string AnswerCallback;

};

[System.Serializable]
public class DialogueWrapper
{

    public DialogueWrapper(string DT, AnswerWrapper[]  DA)
    {
        DialogueText = DT;
        DialogueAnswers = DA;
    }

    public string DialogueText;
    public Texture Portrait;
    public AnswerWrapper[] DialogueAnswers;
	
}

static public class DialogueData
{

    static private AnswerWrapper EmptyAnswer = new AnswerWrapper("", null, KeyCode.None, "");
    static private AnswerWrapper[] EmptyAnswers = new AnswerWrapper[] { EmptyAnswer, EmptyAnswer };

    static private AnswerWrapper A_Landlord_1_Yes = new AnswerWrapper("Yes", null, KeyCode.Return, "A_Landlord_1_Yes");
    static private AnswerWrapper A_Landlord_1_No = new AnswerWrapper("No", null, KeyCode.Escape, "A_Landlord_1_No");

    static public DialogueWrapper Landlord_1_a = new DialogueWrapper("What's wrong with you dude?", EmptyAnswers);
    static public DialogueWrapper Landlord_1_b = new DialogueWrapper("When will you pay me that rent?", EmptyAnswers);
    static public DialogueWrapper Landlord_1_c = new DialogueWrapper("Do me a favor and I won't increase it next week.", EmptyAnswers);
    static public DialogueWrapper Landlord_1_d = new DialogueWrapper("What do you say?", new AnswerWrapper[] { A_Landlord_1_Yes, A_Landlord_1_No });

    static public DialogueWrapper Landlord_2_a = new DialogueWrapper("See those gulls?", EmptyAnswers);
    static public DialogueWrapper Landlord_2_b = new DialogueWrapper("Get rid of 5 of them and come back to me once done.", EmptyAnswers);

    static private AnswerWrapper A_Drunkard_1 = new AnswerWrapper("", null, KeyCode.Return, "A_Drunkard_1");

    static public DialogueWrapper Drunkard_1_a = new DialogueWrapper("YeeWweY Mannn!", EmptyAnswers);
    static public DialogueWrapper Drunkard_1_b = new DialogueWrapper("Ye not local right? So I'll give you a hint...", EmptyAnswers);
    static public DialogueWrapper Drunkard_1_c = new DialogueWrapper("Those flying rats hat cans. AhahAhahHahAh! Burrrrppp...", new AnswerWrapper[] { A_Drunkard_1, EmptyAnswer });

    static private AnswerWrapper A_Landlord_3 = new AnswerWrapper("", null, KeyCode.Return, "A_Landlord_3");

    static public DialogueWrapper Landlord_3_a = new DialogueWrapper("Well done kid.", EmptyAnswers);
    static public DialogueWrapper Landlord_3_b = new DialogueWrapper("By the way, did you know doggies hold a terrible secret?", EmptyAnswers);
    static public DialogueWrapper Landlord_3_c = new DialogueWrapper("If I were you, I would ask them what is this all about...", new AnswerWrapper[] { A_Landlord_3, EmptyAnswer });

    static private AnswerWrapper A_Doggo_1_Yes = new AnswerWrapper("Yes", null, KeyCode.Return, "A_Doggo_1_Yes");
    static private AnswerWrapper A_Doggo_1_No = new AnswerWrapper("No", null, KeyCode.Escape, "A_Doggo_1_No");

    static public DialogueWrapper Doggo_3_a = new DialogueWrapper("Weeef weeeef! Hello friend!", EmptyAnswers);
    static public DialogueWrapper Doggo_3_b = new DialogueWrapper("Did you know -weef- every doggy friendly puppy can foresee your future?", EmptyAnswers);
    static public DialogueWrapper Doggo_3_c = new DialogueWrapper("Do you want to see the world through my eyes?", new AnswerWrapper[] { A_Doggo_1_Yes, A_Doggo_1_No });

    static public string Objective_1 = "Talk to your landlord.";
    static public string Objective_2 = "Find the barfly.";
    static public string Objective_3 = "Collect empty beer bottles.";
    static public string Objective_4 = "Shoot some seagulls.";
    static public string Objective_5 = "Report to the landlord.";
    static public string Objective_6 = "Discover the dog's secret.";
    static public string Objective_7 = "Survive the apocalypse.";

}
