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
  [SerializeField] private
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
    string translatedText = translator.TranslateTextToSymbols(text);
    Debug.Log("Translated dialog text: " + translatedText);
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
    for (int i = 0; i < node.choices.Count; i++)
    {
      int idx = i;
      var ch = node.choices[i];

      var go = Instantiate(choiceButtonPrefab, choicesContainer);
      var view = go.GetComponent<ChoiceButtonView>() ?? go.AddComponent<ChoiceButtonView>();
      view.Init(DialogManager.Instance, idx, settings);
      view.SetHotkey(string.Empty);
      //aqui preguntar al translator, que me de el string traducido segun la información
      string choiceText = translator.TranslateTextToEnglishWithPlayerDict(ch.answerText);

      //mirar en el dialog manager si hay ese string en las opciones
      //si existe asignar el idx verdadero
      if (!DialogManager.Instance.isChoiceInChoiceNode(choiceText))
      {
        //si no default->ultima opcion, ultimo de node.choices
        idx = node.choices.Count - 1;
      }

      view.SetContent(choiceText, string.Empty, /*interactable*/ true, () => onPick?.Invoke(idx));
    }

    SetChoicesVisible(true);

  }

}
