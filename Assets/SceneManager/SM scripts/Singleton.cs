
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T instance = null;
	public static T Instance{get{return instance;}}

	protected virtual void Awake() {
		if(instance != null && this.gameObject != null)
		{
			Destroy(this.gameObject);
		}else{
			instance = (T)this;
		}
		
		if(!gameObject.transform.parent) // neu object ko co folder cha thi ko bi huy
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
