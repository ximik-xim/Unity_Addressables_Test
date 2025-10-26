# Unity_Plugin_Scene_Level_Manager_Public
Плагин предназначен для работы с логикой уровней. Может работать как с Addressable сценами, так и с обычными сценами.
Может автоматически определять ключ текущей сцены, автоматически нумеровать сцены, определять текстуру и цвет обложки уровня, определять ключ следующей сцены.
Есть логика исключений, благодаря которой можно определенной сцене задать уникальный префаб, с уникальной логикой или задать уникальную текстуру, цвет, или даже уникальный порядковый номер сцены.

---------------------------------------------------------------------------------------------------------

Так же плагин можно исп. и без Addressable, но для этого из плагина нужно удалить след. папки

Scene Level Manager -> Addressable Scene Scripts

Scene Level Manager -> Example Scene -> Example Scene Addressable

Scene Level Manager -> Example Scene -> Scripts -> Scene Scripts -> Addressable Scene Scripts

---------------------------------------------------------------------------------------------------------

Примеры сцен находяться по пути

Для Addressable

Scene Level Manager -> Example Scene -> Example Scene Addressable -> Example Scene Dont Destroy (Addressable)

Для Default

Scene Level Manager -> Example Scene -> Example Scene Default -> Example Scene Dont Destroy (Default)

---------------------------------------------------------------------------------------------------------
 
Для запуска сцен нужно пометить как Addressable следующие сцен (на их ключи в Addressable без разницы)

Scene Level Manager -> Example Scene -> Example Scene Addressable -> Example Scene Menu (Default)

Scene Level Manager -> Example Scene -> Example Scene Addressable -> Scene Level -> Example Scene Level 1 (Addressable)

Scene Level Manager -> Example Scene -> Example Scene Addressable -> Scene Level -> Example Scene Level 2 (Addressable)

Так же добавить в сборку след. сцены

Scene Level Manager -> Example Scene -> Example Scene Addressable -> Example Scene Dont Destroy (Addressable)

Scene Level Manager -> Example Scene -> Example Scene Default -> Example Scene Dont Destroy (Default)

Scene Level Manager -> Example Scene -> Example Scene Default -> Example Scene Menu (Default)

Scene Level Manager -> Example Scene -> Example Scene Default -> Scene Level -> Example Scene Level 1 (Default)

Scene Level Manager -> Example Scene -> Example Scene Default -> Scene Level -> Example Scene Level 2 (Default)

---------------------------------------------------------------------------------------------------------

Для работы нужны плагины

Unity_Plugin-General_Public - https://github.com/ximik-xim/Unity_Plugin-TList_Public

Unity_Plugin-TList_Public - https://github.com/ximik-xim/Unity_Plugin-General_Public

Unity_Plugin_Task_Loader_UI_Public - https://github.com/ximik-xim/Unity_Plugin_Task_Loader_UI_Public

Unity_Plugin_List_Task_Public - https://github.com/ximik-xim/Unity_Plugin_List_Task_Public

Unity_Plugin_Save_Data_Public - https://github.com/ximik-xim/Unity_Plugin_Save_Data_Public

Unity_Plugin_Wrapper_Addressables_Manager_Public - https://github.com/ximik-xim/Unity_Plugin_Wrapper_Addressables_Manager_Public

---------------------------------------------------------------------------------------------------------

Ну и так же нужно восстановить ключи TList из бэкапов. Для этого нажимаем на папку Scene Level Manager кнопкой ПКМ и заходим в пункт TList -> К папке -> Стереть и восстановить все данные
