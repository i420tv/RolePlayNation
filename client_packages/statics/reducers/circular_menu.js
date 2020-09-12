var initial = {
    entity_id: -1,
    entity_type: "",
    select_type: "",
    select_id: -1,
    faction: "none",
    select: -1,
}

export default (state = initial, action) => {
    if (action.type === 'CIRCULAR_MENU_FACTION') {
        return {
            ...state,
            faction: action.faction
        }
    } else if (action.type === 'CIRCULAR_MENU_SELECT') {
        return {
            ...state,
            select: action.select,
        }
    } else if (action.type === 'CIRCULAR_MENU_SELECT_ENTITY') {
        return {
            ...state,
            select_id: action.select_id,
            select_type: action.select_type,
            select: -1
        }
    } else if (action.type === 'CIRCULAR_MENU_ENTITY') {
        return {
            ...state,
            entity_id: action.entity_id,
            entity_type: action.entity_type,
        }
    }
    return state;
}