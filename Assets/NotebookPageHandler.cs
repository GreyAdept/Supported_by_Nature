using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class NotebookPageHandler : MonoBehaviour
{

    public GameObject[] bookPages;
    [SerializeField] private List<GameObject> newPages = new List<GameObject>();
    [SerializeField] private int currentPage;

    private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPage = 0;
        audio = GetComponent<AudioSource>();
        GeneratePages();
        JumpToPage(currentPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePages()
    {
        foreach (GameObject page in bookPages)
        {
            var newPage = Instantiate(page, this.transform.position, Quaternion.identity);
            newPage.transform.SetParent(this.transform);
            newPage.transform.localScale = new Vector3(1, 1, 1);
            newPage.gameObject.SetActive(false);
            newPages.Add(newPage);
        }
    }

    public void NextPage()
    {
        if (currentPage < bookPages.Length - 1)
        {
            currentPage++;
        }
        else if (currentPage == bookPages.Length - 1)
        {
            currentPage = 0;
        }
        
        JumpToPage(currentPage);
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
        else if (currentPage == 0)
        {
            currentPage = bookPages.Length - 1;
        }
        
        JumpToPage(currentPage);
    }

    public void JumpToPage(int page)
    {   
        foreach (GameObject pg in newPages)
        {
            pg.SetActive(false);
        }

        if (!audio.isPlaying)
        {
            audio.Play();
        }
        
        newPages[page].gameObject.SetActive(true);
        currentPage = page;
    }

    public void CloseNotebook()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
