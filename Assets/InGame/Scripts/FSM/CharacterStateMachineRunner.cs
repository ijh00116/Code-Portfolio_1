using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachineRunner : MonoBehaviour
{
	IStateCallbackListener statemachine;
	public void Initialize(IStateCallbackListener _statemachine)
	{
		statemachine = _statemachine;
	}

	private void Update()
	{
		if (statemachine == null)
			return;

		statemachine.stateCallback?.OnUpdate?.Invoke();
	}
}
