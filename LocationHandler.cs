using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationsHandler : MonoBehaviour
{
    public Text LocationText;
    // Start is called before the first frame update
    void Start()
    {
    
        var location = transform.GetComponent<Dropdown>();

        location.options.Clear();

        List<string> items = new List<string>();
        items.Add("Item 1");
        items.Add("Item 2");

        foreach(var item in items)
        {
            location.options.Add(new Dropdown.OptionData() { text = item });
        }
        LocationsItemSelected(location);
        location.onValueChanged.AddListener(delegate { LocationsItemSelected(location); });
    
    }

    void LocationsItemSelected (Dropdown location)
    {
        int index = location.value;
        LocationText.text = location.options[index].text;
    }

}




