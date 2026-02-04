using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem.Runtime.Core;
using DialogSystem.Runtime.Models.Nodes;
using DialogSystem.Runtime.Settings.Panels;
using DialogSystem.Runtime.UI;
using UnityEngine;

public class DialogSelector : DialogUIController
{
  //necesita la referencia al translator
  [SerializeField] private int maxOptions = 4;
  [SerializeField]
  private
  Translator translator;
  //DialogManager necesita override para tomar el texto en ingles y traducirlo a la traducción del jugador

  //DialogSelector->Get text(descifrado por el jugador)

  //SOLO RENDERIZA 4 opciones,previo a renderizar las opciones se necesita saber que conoce el jugador
  //construye prefabs de botones y les da el componente choicebuttonview, va a tocar hacer override para llamar antes al que gestiona la lógica 
  //de las traducciones(Translator??, si es correcto usar el idx de button choice y si no usar el ultimo idx de las opciones)
  //
  public override void SetText(string text)
  {
    //antes de setear el texto, traducirlo
    string translatedText = translator.TranslateTextToSymbolsReal(text);
    //    Debug.Log("Translated dialog text: " + translatedText);

    base.SetText(translatedText);
  }
  public override void BuildChoices(ChoiceNode node, DialogChoiceSettings settings, Action<int> onPick)
  {
    if (!choicesContainer || !choiceButtonPrefab)
    {
      if (doDebug) Debug.LogError("[DialogUIController] Choices UI not assigned.");
      return;
    }

    // Clear
    for (int i = choicesContainer.childCount - 1; i >= 0; i--)
      Destroy(choicesContainer.GetChild(i).gameObject);

    // Build
    int optionCount = Mathf.Min(node.choices.Count, maxOptions);

    for (int i = 0; i < optionCount; i++)
    {
      int idx = i;
      var ch = node.choices[i];

      var go = Instantiate(choiceButtonPrefab, choicesContainer);
      var view = go.GetComponent<ChoiceButtonView>() ?? go.AddComponent<ChoiceButtonView>();

      view.SetHotkey(string.Empty);
      //aqui preguntar al translator, que me de el string traducido segun la información
      Debug.Log("Original choice text: " + ch.answerText);
      string choiceText = translator.TranslateTextToSymbolsPlayer(ch.answerText);


      //mirar en el dialog manager si hay ese string en las opciones
      //si existe asignar el idx verdadero
      string englishPlayersChoice = translator.TranslateTextToEnglishPlayer(ch.answerText);

      int newIndex = choiceIndexOfText(englishPlayersChoice, node);
      
      view.Init(DialogManager.Instance, newIndex, settings);

      Debug.Log("Original choice text: " + ch.answerText);
      Debug.Log("Translated into player's symbols choice text: " + choiceText);
      Debug.Log("Real english choice player would respond with: " + englishPlayersChoice + " (option: " + newIndex + ")");
      //Aqui poner que si el texto tiene una traduccion sin solucion no se pueda pulsar
      view.SetContent(choiceText, string.Empty, /*interactable*/ true, () => onPick?.Invoke(newIndex));
    }

    SetChoicesVisible(true);
    //mix choices container children
    // for (int i = 0; i < choicesContainer.childCount; i++)
    // {
    //   var child = choicesContainer.GetChild(i);
    //   child.SetSiblingIndex(UnityEngine.Random.Range(0, choicesContainer.childCount));
    // }

  }
  public int choiceIndexOfText(string text, ChoiceNode currentChoice)
  {
    if (currentChoice == null)
    {
      Debug.Log("Esto no deberia pasar");

      return -1;
    }
    for (int i = 0; i < currentChoice.choices.Count; i++)
    {
      if (currentChoice.choices[i].answerText.Equals(text))
      {
        Debug.Log("[DialogSelector] Found matching choice text at index: " + i + " with text: " + text);
        return i;
      }
    }

    return currentChoice.choices.Count - 1; // Default to last option if not found
  }

}
