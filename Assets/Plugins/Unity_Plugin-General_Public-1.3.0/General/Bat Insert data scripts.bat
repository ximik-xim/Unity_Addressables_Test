@echo off

setlocal

set "unity_project_path="

echo.
echo "Введите путь к папке проекта Unity:"
set /p unity_project_path="> "

if "%unity_project_path%"=="" (
    echo.
    echo "Путь к папке проекта Unity не указан. Завершение..."
    goto end
)

echo.
echo "Добавляю using TListPlugin в файлы скриптов..."

for /r "%unity_project_path%" %%f in (*.cs *.js *.ts *.boo *.mli *.fs) do (
    echo.
    echo "Обработка файла: %%f"
    if not "%%~nf"=="AssemblyInfo" (
        echo using TListPlugin; >> "%%~ff"
    ) else (
        echo "Файл AssemblyInfo пропущен."
    )
)

echo.
echo "Готово!"

end:
echo.
pause