using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StateMachine : MonoBehaviour
{

    [SerializeField]
    protected GameObject[] StatePrefabs;
    protected Dictionary<EStateType, BaseState> StateInstances;

    protected Character OwnerCharacter;

    [HideInInspector]
    public EStateType PreviousState { get { return m_prevState; } }
    protected EStateType m_prevState;

    [HideInInspector]
    public EStateType CurrentState { get { return m_currState; } }
    protected EStateType m_currState;

    // Debug
    protected Text DebugText;


    private void Start()
    {
        DebugText = FindObjectOfType<Text>();
    }

    private void Update()
    {
        if (DebugText != null)
        {
            DebugText.text = m_currState.ToString();
        }
    }

    public void Init(Character OwnerToSet)
    {
        OwnerCharacter = OwnerToSet;

        StateInstances = new Dictionary<EStateType, BaseState>();
        foreach (GameObject StateTemplate in StatePrefabs)
        {
            BaseState InstanciatedState = Instantiate<GameObject>(StateTemplate).GetComponent<BaseState>();
            InstanciatedState.transform.SetParent(transform);
            InstanciatedState.name = "state_" + InstanciatedState.GetStateType().ToString();
            InstanciatedState.gameObject.SetActive(false);
            InstanciatedState.Init(this, OwnerToSet);

            if (!StateInstances.ContainsKey(InstanciatedState.GetStateType()))
            {
                StateInstances.Add(InstanciatedState.GetStateType(), InstanciatedState);
            }
            else
            {
                Debug.LogError("State Type already instantiated.");
            }        
        }

        m_currState = EStateType.None;
        ChangeState(EStateType.Idle);
    }    

    public void ChangeState(EStateType StateType)
    {
        if (StateType != m_currState && StateInstances.ContainsKey(StateType))
        {
            if(m_currState != EStateType.None)
            {
                StateInstances[m_currState].OnExit(StateType);
                StateInstances[m_currState].gameObject.SetActive(false);
            }            

            m_prevState = m_currState;
            m_currState = StateType;

            StateInstances[m_currState].OnEnter();
            StateInstances[m_currState].gameObject.SetActive(true);
        }
    }

    public bool RequestChangeState(EStateType StateType)
    {
        if (StateType != m_currState && StateInstances.ContainsKey(StateType))
        {
            if (GetStateInstance(StateType).CanEnter(m_prevState))
            {
                ChangeState(StateType);
                return true;
            }
        }

        return false;
    }

    public BaseState GetStateInstance(EStateType StateType)
    {
        if (StateInstances.ContainsKey(StateType))
        {
            return StateInstances[StateType];
        }
        return null;
    }

    public void HandleMovementInput(Vector2 PlayerInput)
    {
        if(StateInstances != null && StateInstances.ContainsKey(m_currState))
        {
            StateInstances[m_currState].HandleMovementInput(PlayerInput);
        }        
    }

}
