using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFiller
{

    private static GameplayManager.StoryNode CreateNode(string history, string[] options)
    {
        GameplayManager.StoryNode node = new GameplayManager.StoryNode();
        node.history = history;
        node.answers = options;
        node.nextNode = new GameplayManager.StoryNode[options.Length];
        return node;
    }

    public static GameplayManager.StoryNode FillStory()
    {
        GameplayManager.StoryNode root = CreateNode(
            "Te encuentras en una habitación y no recuerdas nada. Quieres salir",
            new string[] {
            "Explorar objetos",
            "Explorar habitación"});

        GameplayManager.StoryNode nodo1 = CreateNode(
            "Hay una silla y una mesa con una planta a la izquierda. " +
            "A la derecha hay una estanteria con libros. " +
            "Detrás parece que hay cunas cajas.",
            new string[] {
            "Ir a la derecha",
            "Ir a la izquierda",
            "Ir hacia atrás",
            "Explorar habitación"});

        GameplayManager.StoryNode nodo2 = CreateNode(
            "Nada ineresante... " +
            "Aunque hay un libro que llama la atención... " +
            "¿Botánica para astronautas?",
            new string[] {
            "Explorar el resto de la habitación",
            "Averiguar más del libro raro."});

        GameplayManager.StoryNode nodo3 = CreateNode(
           "Parece que habla de plantas, qué sorpresa. " +
           "Hay una señalada, se llama plantus corrientis.",
           new string[] {
            "Dejar el libro en su sitio y explorar el resto de objetos de la habitación."});

        GameplayManager.StoryNode nodo4 = CreateNode(
           "Nada interesante en las cajas, están llenas de libros... " +
           "Deberían estar en la estantería.",
           new string[] {
            "Volver y explorar el resto de objetos de la habitación."});

        GameplayManager.StoryNode nodo5 = CreateNode(
           "Humm, una silla. Te duele la cabeza, así que te sientas.",
           new string[] {
            "Quiero ver lo de la mesa también."});

        GameplayManager.StoryNode nodo6 = CreateNode(
            "La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. " +
            "Los cajones de la mesa parecen entreabiertos.",
            new string[] {
            "Explorar los cajones.",
            "Volver a explorar los otros objetos"});

        GameplayManager.StoryNode nodo6bis = CreateNode(
           "La mesa en sí no tiene nada de especial, tiene un poco de tierra de la planta. " +
           "La etiqueta de la planta pone plantus corrientis. " +
           "Los cajones de la mesa parecen entreabiertos.",
           new string[] {
            "Explorar los cajones.",
            "Mirar la planta de cerca.",
            "Volver a explorar los otros objetos"});

        GameplayManager.StoryNode nodo7 = CreateNode(
          "Los cajones están vacíos, mejor explorar otra cosa.",
          new string[] {
            "Volver a explorar los otros objetos"});

        GameplayManager.StoryNode nodo8 = CreateNode(
           "¡¡Al levantar la planta encuentras una llave!! ¿Qué abrirá?",
           new string[] {
            "Explorar la habitación"});

        GameplayManager.StoryNode nodo9 = CreateNode(
          "La habitación tiene un par de ventanas y una puerta.",
          new string[] {
            "Ira la ventana #1",
            "Ira la ventana #2",
            "Ira la puerta"});

        GameplayManager.StoryNode nodo10 = CreateNode(
          "La ventana está tapiada, no se puede abrir.",
          new string[] {
            "Ira la otra ventana",
            "Ira la puerta"});

        GameplayManager.StoryNode nodo11 = CreateNode(
          "La puerta está cerrada con un candado.",
          new string[] {
            "Explorar los objetos de la habitación"});

        GameplayManager.StoryNode nodo11bis = CreateNode(
          "La puerta está cerrada con un candado.",
          new string[] {
            "Explorar los objetos de la habitación",
            "Usar la llave"});

        GameplayManager.StoryNode nodo12 = CreateNode(
          "¡¡Has salido de la habitación!!",
          new string[] {
            "Salir del juego"});



        root.nextNode[0] = nodo1;
        root.nextNode[1] = nodo9;

        nodo1.nextNode[0] = nodo2;
        nodo1.nextNode[1] = nodo5;
        nodo1.nextNode[2] = nodo4;
        nodo1.nextNode[3] = nodo9;

        nodo2.nextNode[0] = nodo1;
        nodo2.nextNode[1] = nodo3;

        nodo3.nextNode[0] = nodo1;
        nodo3.nodeVisited = () =>
        {
            nodo5.nextNode[0] = nodo6bis;
        };

        nodo4.nextNode[0] = nodo1;

        nodo5.nextNode[0] = nodo6;

        nodo6.nextNode[0] = nodo7;
        nodo6.nextNode[1] = nodo1;

        nodo6bis.nextNode[0] = nodo8;
        nodo6bis.nextNode[1] = nodo1;

        nodo7.nextNode[0] = nodo1;

        nodo8.nextNode[0] = nodo9;
        nodo8.nodeVisited = () =>
        {
            nodo9.nextNode[2] = nodo10.nextNode[1] = nodo11bis;
        };

        nodo9.nextNode[0] = nodo9.nextNode[1] = nodo10;
        nodo9.nextNode[2] = nodo11;

        nodo10.nextNode[0] = nodo10;
        nodo10.nextNode[1] = nodo11;

        nodo11.nextNode[0] = nodo1;

        nodo11bis.nextNode[0] = nodo1;
        nodo11bis.nextNode[1] = nodo12;

        nodo12.isFinal = true;


        return root;
    }
}
