// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections.Generic;
using IslandGame.PuzzleEngine;
using UnityEngine;

public class DIContainer : MonoBehaviour
{
	private Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

	private void Awake()
	{
		Register<ILevelReader>(new PuzzleLevelReader());
	}

	public void Register<T>(T implementation)
	{
		_dependencies[typeof(T)] = implementation;
	}

	public T Resolve<T>()
	{
		object implementation;
		if (_dependencies.TryGetValue(typeof(T), out implementation))
		{
			return (T)implementation;
		}
		throw new Exception($"Dependency of type {typeof(T)} is not registered.");
	}
}
