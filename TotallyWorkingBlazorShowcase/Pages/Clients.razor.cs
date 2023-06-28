using Microsoft.AspNetCore.Components;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public partial class ClientsModel : ComponentBase
    {
        public bool showImage = true; // Изначально показываем изображение
        public string buttonText = "Показать дерево"; // Изначальный текст кнопки

        public void ToggleContent()
        {
            showImage = !showImage; // Меняем тип контента, который нужно показать

            // Меняем текст кнопки в зависимости от текущего состояния
            buttonText = showImage ? "Показать дерево" : "Показать список";
        }
    }
}