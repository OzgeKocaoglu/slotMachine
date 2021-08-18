using System;
using System.Collections.Generic;

public class Reel{

    List<Item> items = new List<Item>();
    private int numberOfcomboItems;

    public Reel(int numberOfcomboItems, List<Item> itemList){
        this.numberOfcomboItems = numberOfcomboItems;
    }
}