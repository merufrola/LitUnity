using UnityEngine;
using UnityEditor;
using System.Collections;

public class GoalScript : MonoBehaviour {

	void Start () {
		AddTag ("High");
		AddTag ("Low");
		AddTag ("barrier");
		AddTag ("goal");
		this.gameObject.tag = "goal";
	}
	static void AddTag(string tagname) {
		UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
		if ((asset != null) && (asset.Length > 0)) {
			SerializedObject so = new SerializedObject(asset[0]);
			SerializedProperty tags = so.FindProperty("tags");

			for (int i = 0; i < tags.arraySize; ++i) {
				if (tags.GetArrayElementAtIndex(i).stringValue == tagname) {
					return;
				}
			}

			int index = tags.arraySize;
			tags.InsertArrayElementAtIndex(index);
			tags.GetArrayElementAtIndex(index).stringValue = tagname;
			so.ApplyModifiedProperties();
			so.Update();
		}
	}
}
