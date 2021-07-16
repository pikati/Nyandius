﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


/// <summary>
/// タスクという単位で処理を登録し、
/// TaskTypeを追加することで順番に処理するクラス
/// ※使用例
/// using System;
/// using UnityEngine;
/// 
/// class Test : MonoBehaviour
/// {
///		// ① enum で タスクを定義する
///		enum TaskEnum { Jump, Charge, Tackle };
///		// ② タスクリストの生成
///		TaskList<TaskEnum> _TaskList = new TaskList<TaskEnum>();
///		void Start()
///		{
///			// ③ タスクの登録
///			DefineTask();
///		}
///		
///		void Update()
///		{
///			// タスクが終了したら、次のタスクを登録する
///			if(_TaskList.IsEnd)
///			{
///				// isPlayerFar = IsPlayerFar() // プレイヤーの距離が遠い時 true
///				if (isPlayerFar)
///				{
///					// プレイヤーの距離が遠い時はチャージしてタックル
///					_TaskList.AddTask(TaskEnum.Charge);
///					_TaskList.AddTask(TaskEnum.Tackle);
///				}
///				else
///				{
///					// プレイヤーの距離が近い時はジャンプ
///					_TaskList.AddTask(TaskEnum.Jump);
///				}
///			}
///			
///			// タスクの更新
///			_TaskList.UpdateTask();
///		}
/// 
///		void DefineTask()
///		{
///			④タスクの登録
///			_TaskList.DefineTask(TaskEnum.Jump, OnTaskJumpEnter, OnTaskJumpUpdate, OnTaskJumpExit);
///			_TaskList.DefineTask(TaskEnum.Charge, OnTaskChargeEnter, OnTaskChargeUpdate, OnTaskChargeExit);
///			_TaskList.DefineTask(TaskEnum.Tackle, OnTaskTackleEnter, OnTaskTackleUpdate, OnTaskTackleExit);
///		}
///		
///		void OnTaskJumpEnter()
///		{
///			// Jumpタスク 変更時に1回呼ばれる
///		}
///		
///		bool OnTaskJumpUpdate()
///		{
///			// Jumpタスク 毎フレーム呼ばれる
///			// (戻り値が true の時は タスクが終了する
///			return true;
///		}
///		
///		void OnTaskJumpExit()
///		{
///			// Jumpタスク 終了時に1回呼ばれる
///		}
///		
///		...
/// }
/// </summary>
/// <typeparam name="T"></typeparam>
public class TaskList<T>
{
	#region define
	private class Task
	{
		public T TaskType;
		public Action Enter { get; set; }
		public Func<bool> Update { get; set; }
		public Action Exit { get; set; }

		public Task(T t, Action enter, Func<bool> update, Action exit)
		{
			TaskType = t;
			Enter = enter;
			Update = update ?? delegate { return true; };
			Exit = exit;
		}
	}
	#endregion

	#region field
	/// <summary> 定義されたタスク </summary>
	Dictionary<T, Task> _DefineTaskDictionary = new Dictionary<T, Task>();
	/// <summary> 現在積まれているタスク </summary>
	List<Task> _CurrentTaskList = new List<Task>();
	/// <summary> 現在動作しているタスク </summary>
	Task _CurrentTask = null;
	/// <summary> 現在のIndex番号 </summary>
	int _CurrentIndex = 0;
	#endregion

	#region property
	/// <summary>
	/// 追加されたタスクがすべて終了しているか
	/// </summary>
	public bool IsEnd
	{
		get { return _CurrentTaskList.Count <= _CurrentIndex; }
	}

	/// <summary>
	///  タスクが動いているか
	/// </summary>
	public bool IsMoveTask
	{
		get { return _CurrentTask != null; }
	}

	/// <summary>
	/// 現在のタスクタイプ
	/// </summary>
	public T CurrentTaskType
	{
		get
		{
			if (_CurrentTask == null)
				return default(T);
			return _CurrentTask.TaskType;
		}
	}

	/// <summary>
	/// 追加されているタスクのリスト
	/// </summary>
	public List<T> CurrentTaskTypeList
	{
		get
		{
			return _CurrentTaskList.Select(x => x.TaskType).ToList();
		}
	}

	/// <summary>
	/// 現在のインデックス
	/// </summary>
	public int CurrentIndex
	{
		get { return _CurrentIndex; }
	}
	#endregion

	#region public function
	/// <summary>
	/// 毎フレーム呼ばれる処理
	/// (BehaviourのUpdateで呼ばれる想定)
	/// </summary>
	public void UpdateTask()
	{
		// タスクがなければ何もしない
		if (IsEnd)
		{
			return;
		}

		// 現在のタスクがなければ、タスクを取得する
		if (_CurrentTask == null)
		{
			_CurrentTask = _CurrentTaskList[_CurrentIndex];
			// Enterを呼ぶ
			_CurrentTask.Enter?.Invoke();
		}

		// タスクのUpdateを呼ぶ
		var isEndOneTask = _CurrentTask.Update();

		// タスクが終了していれば次の処理を呼ぶ
		if (isEndOneTask)
		{
			// 現在のタスクのExitを呼ぶ
			_CurrentTask?.Exit();

			// Index追加
			_CurrentIndex++;

			// タスクがなければクリアする
			if (IsEnd)
			{
				_CurrentIndex = 0;
				_CurrentTask = null;
				_CurrentTaskList.Clear();
				return;
			}

			// 次のタスクを取得する
			_CurrentTask = _CurrentTaskList[_CurrentIndex];
			// 次のタスクのEnterを呼ぶ
			_CurrentTask?.Enter();
		}
	}

	/// <summary>
	/// タスクの定義
	/// </summary>
	/// <param name="t"></param>
	/// <param name="enter"></param>
	/// <param name="update"></param>
	/// <param name="exit"></param>
	public void DefineTask(T t, Action enter, Func<bool> update, Action exit)
	{
		var task = new Task(t, enter, update, exit);
		var exist = _DefineTaskDictionary.ContainsKey(t);
		if (exist)
		{
#if UNITY_EDITOR
			Log.Error(GetType(), "{0}は既に追加されています。(登録されませんでした).", t);
#endif
			return;
		}
		_DefineTaskDictionary.Add(t, task);
	}

	/// <summary>
	/// タスクの登録
	/// </summary>
	/// <param name="t"></param>
	public void AddTask(T t)
	{
		Task task = null;
		var exist = _DefineTaskDictionary.TryGetValue(t, out task);
		if (exist == false)
		{
#if UNITY_EDITOR
			Log.Error(GetType(), "{0}のタスクが登録されていないので追加できません.", t);
#endif
			return;
		}
		_CurrentTaskList.Add(task);
	}

	/// <summary>
	/// 強制終了
	/// </summary>
	public void ForceStop()
	{
		if (_CurrentTask != null)
		{
			_CurrentTask.Exit();
		}
		_CurrentTask = null;
		_CurrentTaskList.Clear();
		_CurrentIndex = 0;
	}
	#endregion
}
