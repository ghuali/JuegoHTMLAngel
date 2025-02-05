using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerTileChecker : MonoBehaviour
{
    public List<string> correctOrderEnlace = new List<string> { "Baldosa4", "Baldosa1", "Baldosa2", "Baldosa3", "Baldosa5" };
    public List<string> correctOrderTitulo = new List<string> { "Boton1", "Boton2", "Boton3", "Boton4", "Boton5" };
    public List<string> correctOrderParrafo = new List<string> { "parrafo1", "parrafo2", "parrafo3", "parrafo4", "parrafo5" };

    private List<string> playerOrder = new List<string>();
    public string currentEnunciado = "Enlace";  // Cambiado de private a public
  // Variable privada

    // Propiedad pública para acceder a _currentEnunciado
    public string CurrentEnunciado
    {
        get { return currentEnunciado; }
        private set { currentEnunciado = value; }  // Solo se puede modificar dentro de esta clase
    }

    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI tileText;
    public TextMeshProUGUI enunciadoText;

    public bool isOrderCorrect = false;

    private Dictionary<string, string> tileNames = new Dictionary<string, string>
    {
        { "Baldosa4", "<body>" },
        { "Baldosa1", "<a>" },
        { "Baldosa2", "texto" },
        { "Baldosa3", "< /a >" },
        { "Baldosa5", "</body>" },
        { "Boton1", "<body>" },
        { "Boton2", "<h2>" },
        { "Boton3", "titulo" },
        { "Boton4", "</h2>" },
        { "Boton5", "</body>" },
        { "parrafo1", "<body>" },
        { "parrafo2", "<p>" },
        { "parrafo3", "Texto" },
        { "parrafo4", "</p>" },
        { "parrafo5", "</body>" }
    };

    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();

    private float tileCooldown = 1f;
    private float lastTileTime = 0f;

    private void Start()
    {
        SetOrderForEnunciado("Enlace");
    }

    public void SetOrderForEnunciado(string enunciado)
    {
        playerOrder.Clear();
        tileText.text = "";
        isOrderCorrect = false;
        CurrentEnunciado = enunciado;  // Usamos la propiedad pública

        // Actualiza el texto del enunciado en pantalla
        if (enunciadoText != null)
        {
            switch (CurrentEnunciado)
            {
                case "Enlace":
                    enunciadoText.text = "Necesito un enlace";
                    break;
                case "Titulo":
                    enunciadoText.text = "Necesito un título";
                    break;
                case "Parrafo":
                    enunciadoText.text = "Necesito un párrafo";
                    break;
                default:
                    enunciadoText.text = "Seleccione un enunciado";
                    break;
            }
        }

        Debug.Log($"Enunciado cambiado a: {CurrentEnunciado}");
    }

    public void ResetPlayerOrder()
    {
        playerOrder.Clear();
        tileText.text = "";
        isOrderCorrect = false;
        Debug.Log("Orden del jugador reseteado.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastTileTime < tileCooldown)
        {
            Debug.Log("Cooldown activo, ignorando colisión.");
            return;
        }

        string tileTag = collision.gameObject.tag;
        Debug.Log($"Colisión detectada con: {collision.gameObject.name} (Tag: {tileTag})");

        // Verificar si el tag de la baldosa pertenece a algún enunciado
        if (IsTileValidForAnyEnunciado(tileTag))
        {
            // Verificar si el tag de la baldosa pertenece al enunciado actual
            if (IsTileValidForCurrentEnunciado(tileTag))
            {
                lastTileTime = Time.time;
                playerOrder.Add(tileTag);
                tileText.text += tileNames[tileTag] + " ";

                ChangeTileColor(collision.gameObject, Color.green);

                Debug.Log($"Jugador pisó: {tileTag}");
                Debug.Log($"Orden actual del jugador: {string.Join(", ", playerOrder)}");

                CheckOrder();
            }
            else
            {
                Debug.LogWarning($"La baldosa {tileTag} no es válida para el enunciado actual: {currentEnunciado}.");
                ResetPlayerOrder(); // Resetea el orden si se pisa una baldosa incorrecta
            }
        }
        else
        {
            Debug.LogWarning($"La baldosa {tileTag} no pertenece a ningún enunciado. Ignorando.");
        }
    }

    private bool IsTileValidForAnyEnunciado(string tileTag)
    {
        // Verifica si el tag está en alguna de las listas de órdenes correctos
        return correctOrderEnlace.Contains(tileTag) ||
               correctOrderTitulo.Contains(tileTag) ||
               correctOrderParrafo.Contains(tileTag);
    }

    private bool IsTileValidForCurrentEnunciado(string tileTag)
    {
        if (currentEnunciado == "Enlace")
        {
            return correctOrderEnlace.Contains(tileTag);
        }
        else if (currentEnunciado == "Titulo")
        {
            return correctOrderTitulo.Contains(tileTag);
        }
        else if (currentEnunciado == "Parrafo")
        {
            return correctOrderParrafo.Contains(tileTag);
        }
        else
        {
            Debug.LogError($"Enunciado desconocido: {currentEnunciado}");
            return false;
        }
    }

    private void CheckOrder()
    {
        List<string> expectedOrder;

        if (currentEnunciado == "Enlace")
        {
            expectedOrder = correctOrderEnlace;
        }
        else if (currentEnunciado == "Titulo")
        {
            expectedOrder = correctOrderTitulo;
        }
        else if (currentEnunciado == "Parrafo")
        {
            expectedOrder = correctOrderParrafo;
        }
        else
        {
            Debug.LogError($"Enunciado desconocido: {currentEnunciado}");
            return;
        }

        Debug.Log($"Orden correcto esperado ({currentEnunciado}): {string.Join(", ", expectedOrder)}");
        Debug.Log($"Orden del jugador: {string.Join(", ", playerOrder)}");

        if (playerOrder.Count > expectedOrder.Count)
        {
            Debug.LogError("ERROR: El jugador ha presionado más baldosas de las necesarias. Reseteando.");
            ResetPlayerOrder();
            return;
        }

        bool isCorrect = true;
        for (int i = 0; i < playerOrder.Count; i++)
        {
            if (playerOrder[i] != expectedOrder[i])
            {
                isCorrect = false;
                Debug.Log($"Error en posición {i}: Esperado ({expectedOrder[i]}) vs Recibido ({playerOrder[i]})");
                break;
            }
        }

        if (isCorrect && playerOrder.Count == expectedOrder.Count)
        {
            feedbackText.text = "¡Orden correcto!";
            isOrderCorrect = true;
            Debug.Log("Orden correcto");
        }
        else if (playerOrder.Count >= expectedOrder.Count)
        {
            Debug.Log("Orden incorrecto. Reseteando.");
            ResetPlayerOrder();
        }
    }

    private void ChangeTileColor(GameObject tile, Color newColor)
    {
        Renderer tileRenderer = tile.GetComponent<Renderer>();

        if (tileRenderer != null)
        {
            if (!originalColors.ContainsKey(tile))
            {
                originalColors[tile] = tileRenderer.material.color;
            }

            tileRenderer.material.color = newColor;
            StartCoroutine(ResetTileColor(tile, 1f));
        }
    }

    private IEnumerator ResetTileColor(GameObject tile, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (originalColors.ContainsKey(tile))
        {
            tile.GetComponent<Renderer>().material.color = originalColors[tile];
            originalColors.Remove(tile);
        }
    }
}