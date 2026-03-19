import re
import os
from pathlib import Path

def remove_cpp_comments(content):
    """Удаляет // комментарии, но сохраняет строки"""
    result = []
    lines = content.split('\n')
    count = 0
    
    for line in lines:
        # Простой подход: удаляем // комментарии вне строк
        new_line = ""
        in_string = False
        escape = False
        i = 0
        while i < len(line):
            char = line[i]
            
            if escape:
                new_line += char
                escape = False
                i += 1
                continue
                
            if char == '\\':
                escape = True
                new_line += char
                i += 1
                continue
            
            if char == '"' and not escape:
                in_string = not in_string
                new_line += char
                i += 1
                continue
            
            if not in_string and i < len(line) - 1 and line[i:i+2] == '//':
                # Нашли комментарий
                count += 1
                break  # Остальная часть строки - комментарий
            
            new_line += char
            i += 1
        
        result.append(new_line.rstrip())
    
    return '\n'.join(result), count

def remove_block_comments(content):
    """Удаляет /* */ блочные комментарии"""
    count = 0
    result = []
    i = 0
    in_string = False
    escape = False
    
    while i < len(content):
        char = content[i]
        
        if escape:
            result.append(char)
            escape = False
            i += 1
            continue
        
        if char == '\\' and in_string:
            escape = True
            result.append(char)
            i += 1
            continue
        
        if char == '"' and not escape:
            in_string = not in_string
            result.append(char)
            i += 1
            continue
        
        if not in_string and i < len(content) - 1:
            if content[i:i+2] == '/*':
                # Нашли начало блочного комментария
                count += 1
                # Ищем конец
                end = content.find('*/', i + 2)
                if end != -1:
                    i = end + 2
                    continue
                else:
                    i += 2
                    continue
            elif content[i:i+2] == '*/':
                # Пропускаем остаток блочного комментария
                i += 2
                continue
        
        result.append(char)
        i += 1
    
    return ''.join(result), count

def remove_xml_comments(content):
    """Удаляет <!-- --> XML/XAML комментарии"""
    count = 0
    result = []
    i = 0
    in_string = False
    
    while i < len(content):
        char = content[i]
        
        if char == '"' and (i == 0 or content[i-1] != '\\'):
            in_string = not in_string
            result.append(char)
            i += 1
            continue
        
        if not in_string and i < len(content) - 4 and content[i:i+4] == '<!--':
            # Нашли начало XML комментария
            count += 1
            # Ищем конец
            end = content.find('-->', i + 4)
            if end != -1:
                i = end + 3
                continue
            else:
                i += 4
                continue
        
        result.append(char)
        i += 1
    
    return ''.join(result), count

def remove_cs_comments(content):
    """Удаляет все типы комментариев из C# кода"""
    total = 0
    
    # Сначала удаляем блочные комментарии /* */
    content, block_count = remove_block_comments(content)
    total += block_count
    
    # Затем удаляем // комментарии
    content, cpp_count = remove_cpp_comments(content)
    total += cpp_count
    
    return content, total

def remove_xaml_comments(content):
    """Удаляет XML комментарии из XAML"""
    return remove_xml_comments(content)

def process_file(filepath):
    """Обрабатывает файл и возвращает количество удалённых комментариев"""
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            original_content = f.read()
    except Exception as e:
        print(f"Ошибка чтения {filepath}: {e}")
        return 0
    
    content = original_content
    total_removed = 0
    
    if filepath.endswith('.cs'):
        content, total_removed = remove_cs_comments(original_content)
    elif filepath.endswith('.xaml'):
        content, total_removed = remove_xaml_comments(original_content)
    
    if total_removed > 0:
        try:
            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(content)
        except Exception as e:
            print(f"Ошибка записи {filepath}: {e}")
            return 0
    
    return total_removed

def main():
    base_dir = Path(r"c:\Users\Nikita\source\repos\AutoWorkShop\AutoWorkShop")
    
    # Исключаем файлы из папки Migrations
    excluded_dirs = ['Migrations']
    
    results = []
    
    # Находим все .cs, .xaml, .xaml.cs файлы
    for pattern in ['**/*.cs', '**/*.xaml']:
        for filepath in base_dir.glob(pattern):
            # Проверяем, не в исключённой ли папке
            if any(excluded in str(filepath) for excluded in excluded_dirs):
                continue
            
            # Пропускаем .Designer.cs файлы (автогенерируемые)
            if '.Designer.cs' in str(filepath):
                continue
            
            removed = process_file(str(filepath))
            if removed > 0:
                results.append((str(filepath), removed))
    
    # Вывод результатов
    print("=" * 80)
    print("ИЗМЕНЁННЫЕ ФАЙЛЫ:")
    print("=" * 80)
    
    total = 0
    for filepath, count in sorted(results):
        rel_path = filepath.replace(str(base_dir) + "\\", "")
        print(f"{rel_path}: {count} комментариев удалено")
        total += count
    
    print("=" * 80)
    print(f"ВСЕГО: {len(results)} файлов изменено, {total} комментариев удалено")
    print("=" * 80)

if __name__ == "__main__":
    main()
