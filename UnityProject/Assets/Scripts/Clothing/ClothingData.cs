﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

//[InitializeOnLoad]
[CreateAssetMenu(fileName = "ClothingData", menuName = "ScriptableObjects/ClothingData", order = 1)]
public class ClothingData : ScriptableObject
{
	public GameObject PrefabVariant;

	public EquippedData Base; //Your Basic clothing, If any missing data on any of the other points it will take it from here
	public EquippedData Base_Adjusted; //Variant for if it is Worn differently
	public EquippedData DressVariant; //humm yeah Dresses 
	public List<EquippedData> Variants; //For when you have 1 million colour variants

	public ItemAttributesData ItemAttributes;

	private static ClothFactory ClothFactoryReference;

    public void Awake()
    {
        InitializePool();
    }

	private void OnEnable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		InitializePool();
	}


	public void InitializePool()
	{
		if (ClothFactoryReference == null) { 
			ClothFactoryReference = Object.FindObjectOfType<ClothFactory>();
		}

		if (ClothFactoryReference != null) {
			if (ClothFactoryReference.ClothingStoredData.ContainsKey(this.name)) {
				Logger.LogError("a ClothingData Has the same name as another one name " + this.name + " Please rename one of them to a different name");
			}
			ClothFactoryReference.ClothingStoredData[this.name] = this;
		}

	}



}




[System.Serializable]
public class EquippedData
{
	public SpriteSheetAndData Equipped;
	public SpriteSheetAndData InHandsLeft;
	public SpriteSheetAndData InHandsRight;
	public SpriteSheetAndData ItemIcon;
}