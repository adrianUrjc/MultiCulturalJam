using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem.Runtime.Models.Nodes;
using DialogSystem.Runtime.Settings.Panels;
using DialogSystem.Runtime.UI;
using UnityEngine;

public class DialogUIControllerTextCypher : DialogUIController
{
    //DialogManager necesita override para tomar el texto en ingles y traducirlo a la traducción del jugador

    //DialogSelector->Get text(descifrado por el jugador)

    //SOLO RENDERIZA 4 opciones,previo a renderizar las opciones se necesita saber que conoce el jugador
    //construye prefabs de botones y les da el componente choicebuttonview, va a tocar hacer override para llamar antes al que gestiona la lógica 
    //de las traducciones(Translator??, si es correcto usar el idx de button choice y si no usar el ultimo idx de las opciones)
    //
  public override void BuildChoices(ChoiceNode node, DialogChoiceSettings settings, Action<int> onPick){}

}
