using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnswerWrapper
{

    public AnswerWrapper(string AT, string AI, string AC)
    {
        AnswerText = AT;
        AnswerInput = AI;
        AnswerCallback = AC;
    }

    public string AnswerText;
    public string AnswerInput;
    public string AnswerCallback;

};

[System.Serializable]
public class DialogueWrapper
{

    public DialogueWrapper(string DT, AnswerWrapper[] DA, bool bA1, bool bA2, bool bNA)
    {
        DialogueText = DT;
        DialogueAnswers = DA;
        bAnswer1 = bA1;
        bAnswer2 = bA2;
        bNext = bNA;
    }

    public string DialogueText;
    public AnswerWrapper[] DialogueAnswers;
    public bool bAnswer1;
    public bool bAnswer2;
    public bool bNext;

}

static public class DialogueData
{

    static private AnswerWrapper EmptyAnswer = new AnswerWrapper("", "", "");
    static private AnswerWrapper[] EmptyAnswers = new AnswerWrapper[] { EmptyAnswer, EmptyAnswer };

    static private AnswerWrapper A_Landlord_1_Yes = new AnswerWrapper("Ok...", "A", "A_Landlord_1_Yes");
    static private AnswerWrapper A_Landlord_1_No = new AnswerWrapper("No!", "B", "A_Landlord_1_No");

    static public DialogueWrapper Landlord_1_a = new DialogueWrapper("What's   wrong   with   you   dude?", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_1_b = new DialogueWrapper("When   will   you   pay   me   that   rent?", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_1_c = new DialogueWrapper("Do   me   a   favor   and   I   won't   increase   it   next   week.", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_1_d = new DialogueWrapper("What   do   you   say?", new AnswerWrapper[] { A_Landlord_1_Yes, A_Landlord_1_No }, true, false, false);

    static public DialogueWrapper Landlord_2_a = new DialogueWrapper("See   those   gulls?", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_2_b = new DialogueWrapper("Get   rid   of   5   of   them   and   come   back   to   me   once   done.", EmptyAnswers, false, false, false);

    static private AnswerWrapper A_Drunkard_1 = new AnswerWrapper("Thank   you!", "A", "A_Drunkard_1");

    static public DialogueWrapper Drunkard_1_a = new DialogueWrapper("YeeWweY   Mannn!", EmptyAnswers, false, false, true);
    static public DialogueWrapper Drunkard_1_b = new DialogueWrapper("Ye   not   local   right?   So   I'll   give   you   a   hint...", EmptyAnswers, false, false, true);
    static public DialogueWrapper Drunkard_1_c = new DialogueWrapper("Those   flying   rats   hat   cans.   AhahAhahHahAh!   Burrrrppp...", new AnswerWrapper[] { A_Drunkard_1, EmptyAnswer }, true, false, false);

    static private AnswerWrapper A_Landlord_PMT = new AnswerWrapper("Póg   mo   thóin!", "A", "A_Landlord_PMT");

    static public DialogueWrapper Landlord_3_a = new DialogueWrapper("Hahahahahah!   Well   done   kid.", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_3_b = new DialogueWrapper("But   I'm   afraid   this   is   not   enough...", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_3_c = new DialogueWrapper("I   found   someone   who   could   pay   twice   as   much   as   you   do.", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_3_d = new DialogueWrapper("And   without   even   complaining   about   the   damp   and   rotten   walls!", EmptyAnswers, false, false, true);
    static public DialogueWrapper Landlord_3_e = new DialogueWrapper("Now   get   away   from   here   and   go   play   with   your   four   legged   friends!", new AnswerWrapper[] { A_Landlord_PMT, EmptyAnswer }, true, false, false);

    static private AnswerWrapper A_Doggo_1_Yes = new AnswerWrapper("Why   not!", "A", "A_Doggo_1_Yes");
    static private AnswerWrapper A_Doggo_1_No = new AnswerWrapper("I'll   pass.", "B", "A_Doggo_1_No");

    static public DialogueWrapper Doggo_3_a = new DialogueWrapper("Woof   woof!   Hello   friend!", EmptyAnswers, false, false, true);
    static public DialogueWrapper Doggo_3_b = new DialogueWrapper("Oh...   you   look   sad.   Is   that   because   of   this   greedy   human?", EmptyAnswers, false, false, true);
    static public DialogueWrapper Doggo_3_c = new DialogueWrapper("Did   you   know   -woof-   every   doggy   friendly   puppy   can   foresee   the   future?", EmptyAnswers, false, false, true);
    static public DialogueWrapper Doggo_3_d = new DialogueWrapper("Wanna   see   what   the   future   will   be   like   because   of   such   behavior?   ", new AnswerWrapper[] { A_Doggo_1_Yes, A_Doggo_1_No }, true, true, false);

    static private AnswerWrapper A_DoggoFutur_1 = new AnswerWrapper("Farewell!", "A", "A_DoggoFutur_1");

    static public DialogueWrapper DoggoFutur_3_a = new DialogueWrapper("Woof-   That   was   just   a   dream   ..", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_b = new DialogueWrapper("A   very   strange   dream...", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_c = new DialogueWrapper("...   where   you   experienced   frustration   and   greediness.", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_d = new DialogueWrapper("Don't   forget   -woof-   you're   not   working   to   survive.   Nor   are   you   living   only   to   pay   a   rent.", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_e = new DialogueWrapper("YOU   are   gifted,   granted   with   a   beautiful   opportunity   to   live   on   earth.", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_f = new DialogueWrapper("Breathing   freely   and   happily!", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_g = new DialogueWrapper("Everyone   deserves   to   write   its   OWN   and   UNIQUE   story.", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_h = new DialogueWrapper("Now   clear   you   mind,   go   out,   be   curious,   smile   at   the   world   and   embrace   the   universe...", EmptyAnswers, false, false, true);
    static public DialogueWrapper DoggoFutur_3_i = new DialogueWrapper("Woof   woof   woof!   See   you   soon   my   friend!", new AnswerWrapper[] { A_DoggoFutur_1, EmptyAnswer }, true, false, false);

    static public string Objective_1 = "Talk   to   your   landlord.";
    static public string Objective_2 = "Find   the   barfly.";
    static public string Objective_3 = "Collect   empty   beer   bottles.";
    static public string Objective_4 = "Shoot   some   seagulls.";
    static public string Objective_5 = "Report   to   the   landlord.";
    static public string Objective_6 = "Discover   the   dog's   secret.";
    static public string Objective_7 = "Survive   the   apocalypse.";

}
